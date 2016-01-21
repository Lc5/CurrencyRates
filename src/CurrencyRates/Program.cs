using System;
using System.Net;

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
                    action = (Actions) Enum.Parse(typeof(Actions), args[0], true);
                }

                using (var context = new Context())
                {
                    switch (action)
                    {
                        case Actions.Fetch:
                            //@todo implement Fetch action
                            var nbpService = new Service.Nbp.CurrencyRates(new WebClient());

                            var filenames = nbpService.fetchFilenames();

                            //@todofilter filenames - only new are left

                            var files = nbpService.fetchFiles(filenames);

                            foreach (var file in files) {
                                context.Files.Add(new Entity.File { Name = file.Name, Content = file.Content });
                            }
                            
                            context.SaveChanges();
                       
                            //@todo parse rates

                            break;

                        case Actions.Show:
                            //@todo implement Show action
                            break;
                        default:
                            break;
                    }
                }                  
            }
            catch (Exception e) {
                Console.WriteLine("An error occured: " + e.Message);
            }            
        }
    }
}
