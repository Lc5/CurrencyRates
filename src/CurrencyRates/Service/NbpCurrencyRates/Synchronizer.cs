using CurrencyRates.Entity;
using CurrencyRates.Entity.Comparer;
using CurrencyRates.Service.NbpCurrencyRates.Entity.Collection;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyRates.Service.NbpCurrencyRates
{
    class Synchronizer
    {
        Context Context;

        public Synchronizer(Context context)
        {
            Context = context;
        }

        public void SyncFiles(IEnumerable<File> files)
        {
            foreach (var file in files)
            {
                SyncFile(file);
            }
        }

        public void SyncFile(File file)
        {
            var currencyRateCollection = CurrencyRateCollection.BuildFromXml(file.Content);

            var newCurrencies = currencyRateCollection
                    .Select(cr => new Currency() { Code = cr.CurrencyCode, Name = cr.CurrencyName })
                    .Except(Context.Currencies.Select(c => c).AsEnumerable(), new CurrencyComparer())
                    .Except(Context.Currencies.Local.Select(c => c).AsEnumerable(), new CurrencyComparer());

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
        }
    }
}
