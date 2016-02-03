using CurrencyRates.Extension;
using CurrencyRates.Service.NbpCurrencyRates;
using System;
using System.Net;

namespace CurrencyRates
{
    class Program
    {
        static void Main(string[] args)
        {       
            var action = Enum.Action.Default;

            if (args.Length > 0)
            {
                action = (Enum.Action) System.Enum.Parse(typeof(Enum.Action), args[0], true);
            }

            var output = "";

            try
            {
                using (var context = new Context())
                {
                    var synchronizer = new Synchronizer(context, new FileFetcher(new WebClient()));

                    switch (action)
                    {
                        case Enum.Action.Fetch:
                            synchronizer.SyncFiles();
                            break;

                        case Enum.Action.Process:
                            synchronizer.SyncRatesFromUnprocessedFiles();
                            break;

                        case Enum.Action.Show:
                            output = Show(context);
                            break;

                        default:
                            synchronizer.SyncFiles();
                            synchronizer.SyncRatesFromUnprocessedFiles();
                            output = Show(context);
                            break;
                    }
                }
                     
                Console.WriteLine(output);         
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());             
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static string Show(Context context)
        {
            var rates = context.Rates.FindLatest();
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

            return output;
        }
    }
}


