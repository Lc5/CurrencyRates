using Moq;
using NUnit.Framework;
using System.Linq;

namespace CurrencyRates.Service.NbpCurrencyRates
{
    [TestFixture]
    class FileFetcherTest
    {
        [Test]
        public void TestFetchFilenames()
        {         
            const string filenames =
                "c002z160105\r\n" +
                "h002z160105\r\n" +
                "a002z160105\r\n" +
                "b001z160105\r\n";

            var webClientMock = new Mock<IWebClient>();
            webClientMock.Setup(c => c.DownloadString(It.IsAny<string>())).Returns(filenames);

            var fileFetcher = new FileFetcher(webClientMock.Object);       
            var fetchedFilenames = fileFetcher.FetchFilenames();

            Assert.That(fetchedFilenames, Is.EquivalentTo(new string[] { "a002z160105.xml", "b001z160105.xml" }));         
        }

        [Test]
        public void TestFetchFile()
        {
            const string filename = "filename";
            const string content = "content";

            var webClientMock = new Mock<IWebClient>();
            webClientMock.Setup(c => c.DownloadString(It.Is<string>(s => s.Contains(filename)))).Returns(content);

            var fileFetcher = new FileFetcher(webClientMock.Object);
            var fetchedFile = fileFetcher.FetchFile(filename);

            Assert.That(fetchedFile.Name, Is.EqualTo(filename));
            Assert.That(fetchedFile.Content, Is.EqualTo(content));
        }

        [Test]
        public void TestFetchFiles()
        {
            var filenames = new string[] { "a002z160105.xml", "b001z160105.xml" };
            var webClientMock = new Mock<IWebClient>();

            var fileFetcher = new FileFetcher(webClientMock.Object);
            var fetchedFiles = fileFetcher.FetchFiles(filenames);

            Assert.That(fetchedFiles.Count(), Is.EqualTo(2));
            Assert.That(fetchedFiles.Any(f => f.Name == "a002z160105.xml"));
            Assert.That(fetchedFiles.Any(f => f.Name == "b001z160105.xml"));
        }

        [Test]
        public void TestFetchAllFilesExcept()
        {
            const string filenames =
                "a002z160106\r\n" +
                "b002z160106\r\n" +
                "a001z160105\r\n" +
                "b001z160105\r\n";

            var existingFilenames = new string[] { "a001z160105.xml", "b001z160105.xml" };

            var webClientMock = new Mock<IWebClient>();
            webClientMock.Setup(c => c.DownloadString(It.IsAny<string>())).Returns(filenames);

            var fileFetcher = new FileFetcher(webClientMock.Object);
            var fetchedFiles = fileFetcher.FetchAllFilesExcept(existingFilenames);

            Assert.That(fetchedFiles.Count(), Is.EqualTo(2));
            Assert.That(fetchedFiles.Any(f => f.Name == "a002z160106.xml"));
            Assert.That(fetchedFiles.Any(f => f.Name == "b002z160106.xml"));
        }
    }
}
