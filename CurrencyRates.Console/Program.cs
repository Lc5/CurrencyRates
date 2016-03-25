using CurrencyRates.Base;
using CurrencyRates.Base.Service;
using CurrencyRates.Console.Presentation;
using CurrencyRates.Model;
using CurrencyRates.Model.Query;
using System;

namespace CurrencyRates.Console
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
                using (var container = ContainerBootstrapper.Bootstrap().Container)
                {
                    var context = container.Resolve<Context>();
                    var synchronizer = container.Resolve<Synchronizer>();

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
                            synchronizer.SyncAll();
                            output = RateRenderer.Render(context.Rates.FindLatest());
                            break;
                    }
                }

                System.Console.WriteLine(output);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }      
    }
}


