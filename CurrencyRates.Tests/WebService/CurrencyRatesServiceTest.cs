namespace CurrencyRates.Tests.WebService
{
    using System.Linq;

    using CurrencyRates.Model;
    using CurrencyRates.Model.Entities;
    using CurrencyRates.Tests.TestUtils;
    using CurrencyRates.WebService;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class CurrencyRatesServiceTest
    {
        [Test]
        public void TestFind()
        {
            var context = new Mock<Context>();
            var rates = new[] { new Rate { Id = 123, CurrencyCode = "EUR" } };
            var contextRates = DbSetMockBuilder.Build(rates.AsQueryable());

            contextRates.Setup(cr => cr.Include("File")).Returns(contextRates.Object);
            context.Setup(c => c.Rates).Returns(contextRates.Object);

            var currencyRatesService = new CurrencyRatesService(context.Object);
            var rate = currencyRatesService.Find(123);

            Assert.That(rate, Is.EqualTo(rates.First()));
        }

        [Test]
        public void TestFindLatest()
        {
            var context = new Mock<Context>();
            var rates = new[] { new Rate { CurrencyCode = "EUR" }, new Rate { CurrencyCode = "USD" } };
            var contextRates = DbSetMockBuilder.Build(rates.AsQueryable());

            context.Setup(c => c.Rates).Returns(contextRates.Object);

            var currencyRatesService = new CurrencyRatesService(context.Object);
            var latestRates = currencyRatesService.FindLatest();

            Assert.That(latestRates, Is.EqualTo(rates));
        }
    }
}
