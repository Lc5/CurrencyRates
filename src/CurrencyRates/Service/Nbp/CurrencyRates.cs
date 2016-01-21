using CurrencyRates.Service.Nbp.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;

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

        public IEnumerable<string> fetchFilenames()
        {
            var filenames = WebClient
                .DownloadString(Url + FileListPath)
                .Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(f => f + ".xml");

            return filenames;
        }

        public File fetchFile(string filename)
        {
            //@todo strip all whitespaces
            var content = WebClient.DownloadString(Url + filename);

            var file = new File() { Name = filename, Content = content };

            return file;
        }

        public Collection<File> fetchFiles(IEnumerable<string> filenames)
        {
            var files = new Collection<File>();

            foreach (string filename in filenames)
            {
                files.Add(this.fetchFile(filename));
            }

            return files;
        }

    }
}
