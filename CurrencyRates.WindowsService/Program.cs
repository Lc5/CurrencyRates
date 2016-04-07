namespace CurrencyRates.WindowsService
{
    using System.ServiceProcess;

    using CurrencyRates.Base;
    using CurrencyRates.Base.Services;

    public static class Program
    {
        private static void Main()
        {
            using (var container = ContainerBootstrapper.Bootstrap().Container)
            {
                var servicesToRun = new ServiceBase[] { new Scheduler(container.Resolve<Synchronizer>()) };

                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
