using CurrencyRates.NbpCurrencyRates.Net;
using CurrencyRates.NbpCurrencyRates.Service.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CurrencyRates.NbpCurrencyRates.Service
{
    public class FileFetcher : IFileFetcher
    {
        const string Url = "http://www.nbp.pl/kursy/xml/";
        const string FileListPath = "dir.txt";

        IWebClient WebClient;

        public FileFetcher(IWebClient webClient)
        {
            WebClient = webClient;
        }

        public IEnumerable<File> FetchAllFilesExcept(IEnumerable<string> existingFilenames)
        {
            var filenames = FetchFilenames().Except(existingFilenames);
            var files = new Collection<File>();

            foreach (var filename in filenames)
            {
                files.Add(FetchFile(filename));
            }

            return files;
        }

        IEnumerable<string> FetchFilenames()
        {
            var filenames = WebClient
                .DownloadString(Url + FileListPath)
                .Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Where(s => s.StartsWith("a") || s.StartsWith("b"))
                .Select(f => f + ".xml");

            return filenames;
        }

        File FetchFile(string filename)
        {
            var content = WebClient.DownloadString(Url + filename);

            return new File() { Name = filename, Content = content };
        }
    }
}
