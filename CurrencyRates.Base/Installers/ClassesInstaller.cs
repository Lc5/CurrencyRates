namespace CurrencyRates.Base.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using CurrencyRates.Base.Services;
    using CurrencyRates.Model;
    using CurrencyRates.NbpCurrencyRates.Net;
    using CurrencyRates.NbpCurrencyRates.Services;

    public class ClassesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Context>());
            container.Register(Component.For<IFileFetcher>().ImplementedBy<FileFetcher>());
            container.Register(Component.For<IWebClient>().ImplementedBy<WebClient>());
            container.Register(Component.For<Synchronizer>());
            container.Register(Component.For<System.Net.WebClient>());
        }
    }
}
