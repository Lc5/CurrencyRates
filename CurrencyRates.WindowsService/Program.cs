using Castle.Windsor;
using Castle.Windsor.Installer;
using CurrencyRates.Base.Service;
using System.ServiceProcess;


namespace CurrencyRates.WindowsService
{
    static class Program
    {
        static void Main()
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.InThisApplication());

            var servicesToRun = new ServiceBase[]
            {
                new Scheduler(container.Resolve<Synchronizer>())
            };

            ServiceBase.Run(servicesToRun);
        }
    }
}
