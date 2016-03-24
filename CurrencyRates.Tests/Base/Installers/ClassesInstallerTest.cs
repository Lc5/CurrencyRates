using Castle.Windsor;
using CurrencyRates.Base.Installers;
using CurrencyRates.Base.Service;
using CurrencyRates.Model;
using CurrencyRates.NbpCurrencyRates.Net;
using CurrencyRates.NbpCurrencyRates.Service;
using NUnit.Framework;

namespace CurrencyRates.Tests.Base.Installers
{
    [TestFixture]
    class ClassesInstallerTest
    {
        IWindsorContainer Container;

        [SetUp]
        public void SetUp()
        {
            Container = new WindsorContainer().Install(new ClassesInstaller());
        }

        [Test]
        public void TestComponentsAreRegistered()
        {
            Container.Resolve<Context>();
            Container.Resolve<IFileFetcher>();
            Container.Resolve<IWebClient>();
            Container.Resolve<Synchronizer>();
            Container.Resolve<System.Net.WebClient>();
        }
    }
}
