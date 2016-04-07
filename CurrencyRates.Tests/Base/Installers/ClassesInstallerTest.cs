namespace CurrencyRates.Tests.Base.Installers
{
    using Castle.Windsor;

    using CurrencyRates.Base.Installers;
    using CurrencyRates.Base.Service;
    using CurrencyRates.Model;
    using CurrencyRates.NbpCurrencyRates.Net;
    using CurrencyRates.NbpCurrencyRates.Service;

    using NUnit.Framework;

    using WebClient = System.Net.WebClient;

    [TestFixture]
    public class ClassesInstallerTest
    {
        private IWindsorContainer container;

        [SetUp]
        public void SetUp()
        {
            this.container = new WindsorContainer().Install(new ClassesInstaller());
        }       

        [Test]
        public void TestComponentsAreRegistered()
        {
            this.container.Resolve<Context>();
            this.container.Resolve<IFileFetcher>();
            this.container.Resolve<IWebClient>();
            this.container.Resolve<Synchronizer>();
            this.container.Resolve<WebClient>();
        }
    }
}
