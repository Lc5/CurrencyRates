using System;
using System.Linq;
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
                            var nbpService = new Service.Nbp.CurrencyRates(new WebClient());

                            var filenames = nbpService
                                .FetchFilenames()
                                .Except(context.Files.Select(f => f.Name));

                            var files = nbpService.FetchFiles(filenames);

                            foreach (var file in files) {
                                context.Files.Add(new Entity.File { Name = file.Name, Content = file.Content });
                            }
                            
                            context.SaveChanges();

                            //@todo parse rates
                            var filesToProcess = context.Files.Where(f => f.Processed == false);

                            foreach (var fileToProcess in filesToProcess) {
                                Console.WriteLine(fileToProcess);
                            }

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
                Console.WriteLine("An error occured: " + e);
            }    
        }
    }
}
