using CurrencyRates.Entity;
using Moq;
using NbpServiceEntity = CurrencyRates.Service.NbpCurrencyRates.Entity;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CurrencyRates.Service.NbpCurrencyRates
{
    [TestFixture]
    class SynchronizerTest
    {
        [Test]
        public void TestSyncFiles()
        {
            var existingFiles = new List<File>
            {
                new File() { Name = "a001z160105.xml", Content = "a001z160105.xml content" },
                new File() { Name = "b001z160105.xml", Content = "b001z160105.xml content" }
            };

            var newFiles = new List<NbpServiceEntity.File>
            {
                new NbpServiceEntity.File() { Name = "a002z160106.xml", Content = "a002z160106.xml content" },
                new NbpServiceEntity.File() { Name = "b002z160106.xml", Content = "b002z160106.xml content" }
            };

            var context = new Mock<Context>();
            var fileFetcher = new Mock<IFileFetcher>();

            Mock<DbSet<File>> fileDbSet = BuildDbSetMock(existingFiles.AsQueryable());

            context.Setup(c => c.Files).Returns(fileDbSet.Object);
            fileFetcher.Setup(ff => ff.FetchAllFilesExcept(new string[] { existingFiles[0].Name, existingFiles[1].Name })).Returns(newFiles);

            var synchronizer = new Synchronizer(context.Object, fileFetcher.Object);
            synchronizer.SyncFiles();

            fileDbSet.Verify(c => c.Add(It.Is<File>(f => f.Name == newFiles[0].Name && f.Content == newFiles[0].Content)));
            fileDbSet.Verify(c => c.Add(It.Is<File>(f => f.Name == newFiles[1].Name && f.Content == newFiles[1].Content)));

            context.Verify(c => c.SaveChanges());
        }

        Mock<DbSet<TEntity>> BuildDbSetMock<TEntity>(IQueryable<TEntity> items) where TEntity : class
        {
            var mockSet = new Mock<DbSet<TEntity>>();
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(items.Provider);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(items.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(items.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => items.GetEnumerator());

            return mockSet;
        }
    }
}
