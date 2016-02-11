using CurrencyRates.Library.Entity;
using CurrencyRates.Library.Entity.Comparer;
using CurrencyRates.Library.Service.NbpCurrencyRates.Entity.Collection;
using System.Linq;

namespace CurrencyRates.Library.Service.NbpCurrencyRates
{
    public class Synchronizer
    {
        Context Context;
        IFileFetcher FileFetcher;

        public Synchronizer(Context context, IFileFetcher fileFetcher)
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
            var files = Context.Files.Where(f => !f.Processed).ToList();

            foreach (var file in files)
            {
                SyncRatesFromFile(file);
            }
        }

        void SyncRatesFromFile(File file)
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
