namespace CurrencyRates.Web.Installers
{
    using Castle.Facilities.WcfIntegration;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using CurrencyRates.Web.CurrencyRatesService;

    public class WebServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<WcfFacility>();
            container.Register(Component.For<CurrencyRates.WebService.ICurrencyRatesService>().ImplementedBy<CurrencyRates.WebService.CurrencyRatesService>().AsWcfService());
            container.Register(Component.For<ICurrencyRatesService>().ImplementedBy<CurrencyRatesServiceClient>());
        }
    }
}
