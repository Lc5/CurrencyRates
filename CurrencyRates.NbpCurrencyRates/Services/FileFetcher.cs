namespace CurrencyRates.NbpCurrencyRates.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using CurrencyRates.NbpCurrencyRates.Net;
    using CurrencyRates.NbpCurrencyRates.Services.Entities;

    public class FileFetcher : IFileFetcher
    {
        private const string FileListPath = "dir.txt";

        private const string Url = "http://www.nbp.pl/kursy/xml/";

        private readonly IWebClient webClient;

        public FileFetcher(IWebClient webClient)
        {
            this.webClient = webClient;
        }

        public IEnumerable<File> FetchAllFilesExcept(IEnumerable<string> existingFilenames)
        {
            var filenames = this.FetchFilenames().Except(existingFilenames);
            var files = new Collection<File>();

            foreach (var filename in filenames)
            {
                files.Add(this.FetchFile(filename));
            }

            return files;
        }

        private File FetchFile(string filename)
        {
            var content = this.webClient.DownloadString(Url + filename);

            return new File { Name = filename, Content = content };
        }

        private IEnumerable<string> FetchFilenames()
        {
            var filenames = this.webClient
                .DownloadString(Url + FileListPath)
                .Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Where(s => s.StartsWith("a") || s.StartsWith("b"))
                .Select(f => f + ".xml");

            return filenames;
        }
    }
}
