using CurrencyRates.Base;
using CurrencyRates.Base.Service;
using System.ServiceProcess;

namespace CurrencyRates.WindowsService
{
    static class Program
    {
        static void Main()
        {
            using (var container = ContainerBootstrapper.Bootstrap().Container)
            {
                var servicesToRun = new ServiceBase[]
                {
                    new Scheduler(container.Resolve<Synchronizer>())
                };

                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
