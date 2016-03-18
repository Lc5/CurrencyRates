using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(CurrencyRates.Web.WindsorActivator), "PreStart")]
[assembly: ApplicationShutdownMethodAttribute(typeof(CurrencyRates.Web.WindsorActivator), "Shutdown")]

namespace CurrencyRates.Web
{
    public static class WindsorActivator
    {
        static ContainerBootstrapper Bootstrapper;

        public static void PreStart()
        {
            Bootstrapper = ContainerBootstrapper.Bootstrap();
        }
        
        public static void Shutdown()
        {
            if (Bootstrapper != null)
            {
                Bootstrapper.Dispose();
            }               
        }
    }
}