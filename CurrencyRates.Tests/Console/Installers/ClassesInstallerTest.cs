using Castle.Windsor;
using CurrencyRates.Common.Service;
using CurrencyRates.Console.Installers;
using CurrencyRates.Model;
using CurrencyRates.NbpCurrencyRates.Net;
using CurrencyRates.NbpCurrencyRates.Service;
using NUnit.Framework;

namespace CurrencyRates.Tests.Console.Installers
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
