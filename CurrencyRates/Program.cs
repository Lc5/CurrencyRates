using CurrencyRates.Presentation;
using CurrencyRates.Query;
using CurrencyRates.Service;
using CurrencyRates.Service.NbpCurrencyRates;
using System;

namespace CurrencyRates
{
    public class Program
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
                    using (var systemWebclient = new System.Net.WebClient())
                    {
                        var synchronizer = new Synchronizer(context, new FileFetcher(new WebClient(systemWebclient)));

                        switch (action)
                        {
                            case Enum.Action.Fetch:
                                synchronizer.SyncFiles();
                                break;

                            case Enum.Action.Process:
                                synchronizer.SyncRatesFromUnprocessedFiles();
                                break;

                            case Enum.Action.Show:
                                output = RateRenderer.Render(context.Rates.FindLatest());
                                break;

                            default:
                                synchronizer.SyncFiles();
                                synchronizer.SyncRatesFromUnprocessedFiles();
                                output = RateRenderer.Render(context.Rates.FindLatest());
                                break;
                        }
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
    }
}


