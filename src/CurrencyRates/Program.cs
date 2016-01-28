using CurrencyRates.Extension;
using CurrencyRates.Extension.Linq;
using System;
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
                            context.SaveChanges();
                            Process(context);
                            context.SaveChanges();
                            Show(context);
                            break;
                    }

                    context.SaveChanges();
                }                  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());             
            }
            Console.ReadLine();
        }

        static void Fetch(Context context)
        {
            var nbpService = new Service.NbpCurrencyRates.FileFetcher(new WebClient());
            var files = nbpService.FetchAllFilesExcept(context.Files.Select(f => f.Name));

            foreach (var file in files)
            {
                context.Files.Add(new Entity.File() { Name = file.Name, Content = file.Content });
            }
        }

        static void Process(Context context)
        {
            var synchronizer = new Service.NbpCurrencyRates.Synchronizer(context);
            synchronizer.SyncFiles(context.Files.Where(f => !f.Processed));
        }

        static void Show(Context context)
        {
            var rates = context.Rates
                .OrderByDescending(r => r.Date)
                .DistinctBy(r => r.CurrencyCode)
                .OrderBy(r => r.CurrencyCode)
                .Select(x => x);

            var separator = new String('-', 79) + "\n";
            var format = "| {0, -10} | {1, -40} | {2, 11} | {3, 5} |\n";

            var output = "";

            output += separator;
            output += String.Format(format, "Date", "Currency", "Value", "Multi");     
            output += separator;

            foreach (var rate in rates)
            {
                output += String.Format(format, rate.Date.ToString("dd-MM-yyyy"), rate.CurrencyCode + " " + rate.Currency.Name.Truncate(36), rate.Value + " PLN", rate.Multiplier);
            }

            output += separator;

            Console.Write(output);
        }
    }
}


