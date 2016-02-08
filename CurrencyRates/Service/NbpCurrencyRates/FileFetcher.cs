﻿using CurrencyRates.Service.NbpCurrencyRates.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CurrencyRates.Service.NbpCurrencyRates
{
    public class FileFetcher
    {
        const string Url = "http://www.nbp.pl/kursy/xml/";
        const string FileListPath = "dir.txt";

        IWebClient WebClient;

        public FileFetcher(IWebClient webClient)
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
            var content = WebClient.DownloadString(Url + filename);

            return new File() { Name = filename, Content = content };
        }

        public IEnumerable<File> FetchFiles(IEnumerable<string> filenames)
        {
            var files = new Collection<File>();

            foreach (string filename in filenames)
            {
                files.Add(FetchFile(filename));
            }

            return files;
        }

        public IEnumerable<File> FetchAllFilesExcept(IEnumerable<string> existingFilenames)
        {
            var filenames = FetchFilenames().Except(existingFilenames);

            return FetchFiles(filenames);
        }   
    }
}
