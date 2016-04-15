namespace CurrencyRates.Tests.Web.Installers
{
    using Castle.Facilities.WcfIntegration;
    using Castle.Windsor;

    using CurrencyRates.Tests.TestUtils;
    using CurrencyRates.Web.Installers;

    using NUnit.Framework;

    using ICurrencyRatesService = CurrencyRates.WebService.ICurrencyRatesService;
    using ICurrencyRatesServiceClient = CurrencyRates.Web.CurrencyRatesService.ICurrencyRatesService;

    [TestFixture]
    public class WebServiceInstallerTest
    {
        private IWindsorContainer containerWithWebservice;

        [SetUp]
        public void SetUp()
        {
            this.containerWithWebservice = new WindsorContainer();
            this.containerWithWebservice.Install(new WebServiceInstaller());
        }

        [Test]
        public void TestWebserviceIsRegistered()
        {
            this.containerWithWebservice.Resolve<WcfServiceExtension>();
            Assert.That(this.containerWithWebservice.GetHandlersFor(typeof(ICurrencyRatesService)), Is.Not.Empty);
            Assert.That(this.containerWithWebservice.GetHandlersFor(typeof(ICurrencyRatesServiceClient)), Is.Not.Empty);          
        }
    }
}
