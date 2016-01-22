using CurrencyRates.Service.Nbp.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace CurrencyRates.Service.Nbp
{
    class CurrencyRates
    {
        const string Url = "http://www.nbp.pl/kursy/xml/";
        const string FileListPath = "dir.txt";
        //@todo dispose WebClient after work
        WebClient WebClient;

        public CurrencyRates(WebClient webClient)
        {
            WebClient = webClient;
        }

        public IEnumerable<string> FetchFilenames()
        {
            var filenames = WebClient
                .DownloadString(Url + FileListPath)
                .Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Where(s => s.StartsWith("a") || s.StartsWith("b"))
                .Select(f => f + ".xml");

            return filenames;
        }

        public File FetchFile(string filename)
        {
            var downloadedString = WebClient.DownloadString(Url + filename);
            var content = Regex.Replace(downloadedString, @"\s+", "");

            return new File() { Name = filename, Content = content };
        }

        public IEnumerable<File> FetchFiles(IEnumerable<string> filenames)
        {
            var files = new Collection<File>();

            foreach (string filename in filenames)
            {
                files.Add(this.FetchFile(filename));
            }

            return files;
        }
    }
}
