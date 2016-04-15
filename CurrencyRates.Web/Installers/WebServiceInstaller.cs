namespace CurrencyRates.Web.Installers
{
    using Castle.Facilities.WcfIntegration;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using CurrencyRates.Web.CurrencyRatesService;    
    using CurrencyRates.WebService;

    using ICurrencyRatesService = CurrencyRates.WebService.ICurrencyRatesService;
    using ICurrencyRatesServiceClient = CurrencyRates.Web.CurrencyRatesService.ICurrencyRatesService;

    public class WebServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<WcfFacility>();

            container.Register(
                Component
                    .For<ICurrencyRatesService>()
                    .ImplementedBy<CurrencyRatesService>()
                    .AsWcfService());

            container.Register(
                Component
                    .For<ICurrencyRatesServiceClient>()
                    .ImplementedBy<CurrencyRatesServiceClient>());
        }
    }
}
