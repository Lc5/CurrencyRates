using CurrencyRates.Service.Nbp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CurrencyRates
{
    class Program
    {
        enum Actions { Default, Fetch, Process, Show };

        static void Main(string[] args)
        {
            try
            {
                var action = Actions.Default;

                if (args.Length > 0)
                {
                    action = (Actions) Enum.Parse(typeof(Actions), args[0], true);
                }

                using (var context = new Context())
                {
                    switch (action)
                    {
                        case Actions.Fetch:
                            Fetch(context);
                            break;

                        case Actions.Process:
                            Process(context);
                            break;

                        case Actions.Show:
                            Show(context);
                            break;

                        default:
                            Fetch(context);
                            Process(context);
                            Show(context);
                            break;
                    }

                    context.SaveChanges();
                }                  
            }
            catch (Exception e) {
                Console.WriteLine("An error occured: " + e);             
            }
        }

        static void Fetch(Context context)
        {
            var nbpService = new Service.Nbp.CurrencyRates(new WebClient());

            var filenames = nbpService
                .FetchFilenames()
                .Except(context.Files.Select(f => f.Name));

            var files = nbpService.FetchFiles(filenames);

            foreach (var file in files)
            {
                context.Files.Add(new Entity.File { Name = file.Name, Content = file.Content });
            }
        }

        static void Process(Context context)
        { 
            var files = context.Files.Where(f => !f.Processed);

            foreach (var file in files)
            {
                var currencyRateCollection = CurrencyRateCollection.buildFromXml(file.Content);

                var currenciesToAdd = currencyRateCollection
                    .Select(cr => new Entity.Currency { Code = cr.CurrencyCode, Name = cr.CurrencyName })
                    .Except(context.Currencies.Select(c => c).AsEnumerable(), new CurrencyComparer())
                    .Except(context.Currencies.Local.Select(c => c).AsEnumerable(), new CurrencyComparer());

                context.Currencies.AddRange(currenciesToAdd);

                foreach (var currencyRate in currencyRateCollection)
                {
                    var rate = new Entity.Rate()
                    {
                        Date = currencyRateCollection.PublicationDate,
                        Value = currencyRate.AverageValue,
                        Multiplier = currencyRate.Multiplier,
                        CurrencyCode = currencyRate.CurrencyCode,
                        FileId = file.Id
                    };

                    context.Rates.Add(rate);
                }

                file.Processed = true;
            }
        }

        static void Show(Context context)
        {
            var rates = context.Rates.OrderByDescending(r => r.Date).GroupBy(r => r.CurrencyCode).Select(x => x.FirstOrDefault());

            foreach (var rate in rates)
            {
                Console.WriteLine(rate.Date + " " + rate.CurrencyCode + " " + rate.Value + " " + rate.Multiplier);
            }
        }
    }

    //@todo refactor this
    public class CurrencyComparer : IEqualityComparer<Entity.Currency>
    {
        public bool Equals(Entity.Currency x, Entity.Currency y)
        {
            return x.Code == y.Code;
        }

        public int GetHashCode(Entity.Currency obj)
        {
            return 0;
        }
    }
}


