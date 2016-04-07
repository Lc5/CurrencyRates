namespace CurrencyRates.Tests.Base.Service
{
    using System.Collections.Generic;
    using System.Linq;

    using CurrencyRates.Base.Service;
    using CurrencyRates.Model;
    using CurrencyRates.Model.Entity;
    using CurrencyRates.NbpCurrencyRates.Service;
    using CurrencyRates.Tests.TestUtils;

    using Moq;

    using NUnit.Framework;

    using NbpServiceEntity = CurrencyRates.NbpCurrencyRates.Service.Entity;

    [TestFixture]
    public class SynchronizerTest
    {
        private const string SampleXmlContent = 
            @"<?xml version=""1.0"" encoding=""ISO-8859-2""?>
            <tabela_kursow typ=""A"" uid=""16a011"">
                <numer_tabeli>011/A/NBP/2016</numer_tabeli>
                <data_publikacji>2016-01-19</data_publikacji>
                <pozycja>
                    <nazwa_waluty>dolar amerykański</nazwa_waluty>
                    <przelicznik>1</przelicznik>
                    <kod_waluty>USD</kod_waluty>
                    <kurs_sredni>4,0917</kurs_sredni>
                </pozycja>                 
                <pozycja>
                    <nazwa_waluty>euro</nazwa_waluty>
                    <przelicznik>1</przelicznik>
                    <kod_waluty>EUR</kod_waluty>
                    <kurs_sredni>4,4490</kurs_sredni>
                </pozycja>                 
            </tabela_kursow>";

        [Test]
        public void TestSyncAll()
        {
            var existingFiles = new List<File>
            {
                new File { Name = "a002z160105.xml", Content = SampleXmlContent, Processed = false }
            };

            var currencies = DbSetMockBuilder.Build(Enumerable.Empty<Currency>().AsQueryable());
            var files = DbSetMockBuilder.Build(existingFiles.AsQueryable());
            var rates = DbSetMockBuilder.Build(Enumerable.Empty<Rate>().AsQueryable());
            var context = new Mock<Context>();
            var fileFetcher = new Mock<IFileFetcher>();

            context.Setup(c => c.Currencies).Returns(currencies.Object);
            context.Setup(c => c.Files).Returns(files.Object);
            context.Setup(c => c.Rates).Returns(rates.Object);

            var synchronizer = new Synchronizer(context.Object, fileFetcher.Object);
            synchronizer.SyncAll();

            context.Verify(c => c.SaveChanges(), Times.Exactly(2));
        }

        [Test]
        public void TestSyncFiles()
        {
            var existingFiles = new List<File>
            {
                new File { Name = "a001z160105.xml", Content = "a001z160105.xml content" }, 
                new File { Name = "b001z160105.xml", Content = "b001z160105.xml content" }
            };

            var newFiles = new List<NbpServiceEntity.File>
            {
                new NbpServiceEntity.File { Name = "a002z160106.xml", Content = "a002z160106.xml content" }, 
                new NbpServiceEntity.File { Name = "b002z160106.xml", Content = "b002z160106.xml content" }
            };

            var context = new Mock<Context>();
            var fileFetcher = new Mock<IFileFetcher>();
            var fileDbSet = DbSetMockBuilder.Build(existingFiles.AsQueryable());

            context.Setup(c => c.Files).Returns(fileDbSet.Object);
            fileFetcher.Setup(ff => ff.FetchAllFilesExcept(new[] { existingFiles[0].Name, existingFiles[1].Name })).Returns(newFiles);

            var synchronizer = new Synchronizer(context.Object, fileFetcher.Object);
            synchronizer.SyncFiles();

            fileDbSet.Verify(fds => fds.Add(It.Is<File>(f => f.Name == newFiles[0].Name && f.Content == newFiles[0].Content)));
            fileDbSet.Verify(fds => fds.Add(It.Is<File>(f => f.Name == newFiles[1].Name && f.Content == newFiles[1].Content)));

            context.Verify(c => c.SaveChanges());
        }

        [Test]
        public void TestSyncRatesFromUnprocessedFiles()
        {
            var existingCurrencies = new List<Currency> { new Currency { Code = "USD", Name = "dolar amerykański" } };

            var existingFiles = new List<File>
            {
                new File { Name = "a001z160105.xml", Processed = true }, 
                new File { Name = "b001z160105.xml", Processed = true }, 
                new File { Name = "a002z160105.xml", Content = SampleXmlContent, Processed = false }, 
                new File { Name = "b002z160105.xml", Content = SampleXmlContent, Processed = false }
            };

            var currencies = DbSetMockBuilder.Build(existingCurrencies.AsQueryable());
            var files = DbSetMockBuilder.Build(existingFiles.AsQueryable());
            var rates = DbSetMockBuilder.Build(Enumerable.Empty<Rate>().AsQueryable());
            var context = new Mock<Context>();
            var fileFetcher = new Mock<IFileFetcher>();

            context.Setup(c => c.Currencies).Returns(currencies.Object);
            context.Setup(c => c.Files).Returns(files.Object);
            context.Setup(c => c.Rates).Returns(rates.Object);

            var synchronizer = new Synchronizer(context.Object, fileFetcher.Object);
            synchronizer.SyncRatesFromUnprocessedFiles();

            currencies.Verify(cs => cs.AddRange(It.Is<IEnumerable<Currency>>(c => c.Count() == 1)), Times.Exactly(2));
            rates.Verify(rs => rs.Add(It.IsAny<Rate>()), Times.Exactly(4));
            context.Verify(c => c.SaveChanges(), Times.Exactly(2));

            Assert.That(existingFiles.Count(f => !f.Processed), Is.EqualTo(0));
        }
    }
}
