namespace CurrencyRates.Tests.Web.Installers
{
    using Castle.Windsor;

    using CurrencyRates.Model;
    using CurrencyRates.Web.Installers;

    using NUnit.Framework;

    [TestFixture]
    public class ContextInstallerTest
    {
        private IWindsorContainer containerWithContext;

        [SetUp]
        public void SetUp()
        {
            this.containerWithContext = new WindsorContainer();            
            this.containerWithContext.Install(new ContextInstaller());
        }       

        [Test]
        public void TestContextIsRegistered()
        {
            var context = this.containerWithContext.Resolve<Context>();

            Assert.That(context.Configuration.ProxyCreationEnabled, Is.False);
        }       
    }
}
