using System;

namespace CurrencyRates
{
    class Program
    {
        enum Actions { Fetch, Show };

        static void Main(string[] args)
        {
            try
            {
                var action = Actions.Fetch;

                if (args.Length > 0)
                {
                    action = (Actions)Enum.Parse(typeof(Actions), args[0], true);
                }

                switch (action)
                {
                    case Actions.Fetch:
                        //@todo implement Fetch action
                        break;

                    case Actions.Show:
                        //@todo implement Show action
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e) {
                Console.WriteLine("An error occured: " + e.Message);
            }
        }
    }
}
