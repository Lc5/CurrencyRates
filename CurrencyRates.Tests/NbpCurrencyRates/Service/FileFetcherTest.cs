using CurrencyRates.NbpCurrencyRates.Net;
using CurrencyRates.NbpCurrencyRates.Service;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace CurrencyRates.Tests.NbpCurrencyRates.Service
{
    [TestFixture]
    class FileFetcherTest
    {
        [Test]
        public void TestFetchAllFilesExcept()
        {
            const string filenames =
                "a002z160106\r\n" +
                "b002z160106\r\n" +
                "a001z160105\r\n" +
                "b001z160105\r\n";

            var existingFilenames = new[] { "a001z160105.xml", "b001z160105.xml" };

            var webClientMock = new Mock<IWebClient>();
            webClientMock.Setup(c => c.DownloadString(It.Is<string>(s => s.Contains("dir.txt")))).Returns(filenames);
            webClientMock.Setup(c => c.DownloadString(It.Is<string>(s => s.Contains("a002z160106.xml")))).Returns("a002z160106.xml content");
            webClientMock.Setup(c => c.DownloadString(It.Is<string>(s => s.Contains("b002z160106.xml")))).Returns("b002z160106.xml content");

            var fileFetcher = new FileFetcher(webClientMock.Object);
            var fetchedFiles = fileFetcher.FetchAllFilesExcept(existingFilenames).ToList();

            Assert.That(fetchedFiles.Count, Is.EqualTo(2));
            Assert.That(fetchedFiles.Any(f => f.Name == "a002z160106.xml" && f.Content == "a002z160106.xml content"));
            Assert.That(fetchedFiles.Any(f => f.Name == "b002z160106.xml" && f.Content == "b002z160106.xml content"));
        }
    }
}
