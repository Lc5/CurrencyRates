namespace CurrencyRates.Base.Services
{
    using System;
    using System.Linq;

    using CurrencyRates.Base.Extensions;
    using CurrencyRates.Model;
    using CurrencyRates.Model.Entities;
    using CurrencyRates.Model.Entities.Comparers;
    using CurrencyRates.NbpCurrencyRates.Services;
    using CurrencyRates.NbpCurrencyRates.Services.Entities.Collections;

    public class Synchronizer
    {
        private readonly Context context;

        private readonly IFileFetcher fileFetcher;

        public Synchronizer(Context context, IFileFetcher fileFetcher)
        {
            this.context = context;
            this.fileFetcher = fileFetcher;
        }

        public void SyncAll()
        {
            this.SyncFiles();
            this.SyncRatesFromUnprocessedFiles();
        }

        public void SyncFiles()
        {
            var files = this.fileFetcher.FetchAllFilesExcept(this.context.Files.Select(f => f.Name));

            foreach (var file in files)
            {
                this.context.Files.Add(new File { Name = file.Name, Content = file.Content });
            }

            this.context.SaveChanges();
        }

        public void SyncRatesFromUnprocessedFiles()
        {
            var files = this.context.Files.Where(f => !f.Processed).ToList();

            foreach (var file in files)
            {
                try
                {
                    this.SyncRatesFromFile(file);
                }
                catch (Exception e)
                {
                    e.Log();
                }
            }
        }

        private void SyncRatesFromFile(File file)
        {
            var currencyRateCollection = CurrencyRateCollection.BuildFromXml(file.Content);

            var newCurrencies = currencyRateCollection
                .Select(cr => new Currency { Code = cr.CurrencyCode, Name = cr.CurrencyName })
                .Except(this.context.Currencies.Select(c => c).AsEnumerable(), new CurrencyComparer());

            this.context.Currencies.AddRange(newCurrencies);

            foreach (var currencyRate in currencyRateCollection)
            {
                var rate = new Rate
                {
                    Date = currencyRateCollection.PublicationDate, 
                    Value = currencyRate.AverageValue, 
                    Multiplier = currencyRate.Multiplier, 
                    CurrencyCode = currencyRate.CurrencyCode, 
                    FileId = file.Id
                };

                this.context.Rates.Add(rate);
            }

            file.Processed = true;

            this.context.SaveChanges();
        }
    }
}
