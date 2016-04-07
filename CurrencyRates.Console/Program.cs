namespace CurrencyRates.Console
{
    using System;

    using CurrencyRates.Base;
    using CurrencyRates.Base.Services;
    using CurrencyRates.Console.Presentation;
    using CurrencyRates.Model;
    using CurrencyRates.Model.Queries;

    using Action = CurrencyRates.Console.Enums.Action;

    public static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var action = Action.Default;

                if (args.Length > 0)
                {
                    action = (Action)System.Enum.Parse(typeof(Action), args[0], true);
                }

                var output = string.Empty;

                using (var container = ContainerBootstrapper.Bootstrap().Container)
                {
                    var context = container.Resolve<Context>();
                    var synchronizer = container.Resolve<Synchronizer>();

                    switch (action)
                    {
                        case Action.Fetch:
                            synchronizer.SyncFiles();
                            break;

                        case Action.Process:
                            synchronizer.SyncRatesFromUnprocessedFiles();
                            break;

                        case Action.Show:
                            output = RateRenderer.Render(context.Rates.FindLatest());
                            break;

                        default:
                            synchronizer.SyncAll();
                            output = RateRenderer.Render(context.Rates.FindLatest());
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
    }
}
