namespace CurrencyRates.Tests.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    using CurrencyRates.Model.Entities;
    using CurrencyRates.Web.Controllers;
    using CurrencyRates.Web.CurrencyRatesService;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class RatesControllerTest
    {
        [Test]
        public void TestDetails()
        {
            var currencyRatesService = new Mock<ICurrencyRatesService>();
            var rate = new Rate { Id = 123, CurrencyCode = "EUR" };

            currencyRatesService.Setup(crs => crs.Find(123)).Returns(rate);

            var controller = new RatesController(currencyRatesService.Object);
            var result = controller.Details(123) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(rate));
        }

        [Test]
        public void TestDetailsWithNotExistingId()
        {
            var currencyRatesService = new Mock<ICurrencyRatesService>();

            currencyRatesService.Setup(c => c.Find(123)).Returns((Rate)null);

            var controller = new RatesController(currencyRatesService.Object);
            var result = controller.Details(123);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<HttpNotFoundResult>());
        }

        [Test]
        public void TestDetailsWithNullId()
        {
            var currrencyRatesService = new Mock<ICurrencyRatesService>();
            var controller = new RatesController(currrencyRatesService.Object);

            var result = controller.Details(null) as HttpStatusCodeResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        [Test]
        public void TestIndex()
        {
            var currencyRatesService = new Mock<ICurrencyRatesService>();
            var rates = new[] { new Rate { CurrencyCode = "EUR" }, new Rate { CurrencyCode = "USD" } };

            currencyRatesService.Setup(crs => crs.FindLatest()).Returns(rates);

            var controller = new RatesController(currencyRatesService.Object);
            var result = controller.Index();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(rates));
        }
    }
}
