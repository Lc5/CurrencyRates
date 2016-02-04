using CurrencyRates.Entity;
using CurrencyRates.Entity.Comparer;
using CurrencyRates.Service.NbpCurrencyRates.Entity.Collection;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyRates.Service.NbpCurrencyRates
{
    public class Synchronizer
    {
        Context Context;
        FileFetcher FileFetcher;

        public Synchronizer(Context context, FileFetcher fileFetcher)
        {
            Context = context;
            FileFetcher = fileFetcher;
        }

        public void SyncFiles()
        {
            var files = FileFetcher.FetchAllFilesExcept(Context.Files.Select(f => f.Name));

            foreach (var file in files)
            {
                Context.Files.Add(new File() { Name = file.Name, Content = file.Content });
            }

            Context.SaveChanges();
        }

        public void SyncRatesFromUnprocessedFiles()
        {
            SyncRatesFromFiles(Context.Files.Where(f => !f.Processed));
        }

        public void SyncRatesFromFiles(IEnumerable<File> files)
        {
            foreach (var file in files.ToList())
            {
                SyncRatesFromFile(file);
            }
        }

        public void SyncRatesFromFile(File file)
        {
            var currencyRateCollection = CurrencyRateCollection.BuildFromXml(file.Content);

            var newCurrencies = currencyRateCollection
                    .Select(cr => new Currency() { Code = cr.CurrencyCode, Name = cr.CurrencyName })
                    .Except(Context.Currencies.Select(c => c).AsEnumerable(), new CurrencyComparer());

            Context.Currencies.AddRange(newCurrencies);      

            foreach (var currencyRate in currencyRateCollection)
            {
                var rate = new Rate()
                {
                    Date = currencyRateCollection.PublicationDate,
                    Value = currencyRate.AverageValue,
                    Multiplier = currencyRate.Multiplier,
                    CurrencyCode = currencyRate.CurrencyCode,
                    FileId = file.Id
                };

                Context.Rates.Add(rate);
            }

            file.Processed = true;

            Context.SaveChanges();
        }
    }
}
