namespace CurrencyRates.Web.Installers
{
    using Castle.Facilities.WcfIntegration;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using CurrencyRates.Model;

    public class ContextInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<Context>()
                    .LifestyleTransient()
                    .OnCreate(c => c.Configuration.ProxyCreationEnabled = false));
        }
    }
}
