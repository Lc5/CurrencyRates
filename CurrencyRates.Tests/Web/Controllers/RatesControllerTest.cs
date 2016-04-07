namespace CurrencyRates.Tests.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using CurrencyRates.Model;
    using CurrencyRates.Model.Entity;
    using CurrencyRates.Tests.TestUtils;
    using CurrencyRates.Web.Controllers;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class RatesControllerTest
    {
        [Test]
        public void TestDetails()
        {
            var context = new Mock<Context>();
            var rates = new List<Rate> { new Rate { Id = 123, CurrencyCode = "EUR" } };
            var contextRates = DbSetMockBuilder.Build(rates.AsQueryable());

            contextRates.Setup(cr => cr.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => rates.FirstOrDefault(r => r.Id == (int)ids[0]));

            context.Setup(c => c.Rates).Returns(contextRates.Object);

            var controller = new RatesController(context.Object);
            var result = controller.Details(123) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(rates.First()));
        }

        [Test]
        public void TestDetailsWithNotExistingId()
        {
            var context = new Mock<Context>();
            var contextRates = DbSetMockBuilder.Build(Enumerable.Empty<Rate>().AsQueryable());

            context.Setup(c => c.Rates).Returns(contextRates.Object);

            var controller = new RatesController(context.Object);
            var result = controller.Details(123);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<HttpNotFoundResult>());
        }

        [Test]
        public void TestDetailsWithNullId()
        {
            var context = new Mock<Context>();
            var controller = new RatesController(context.Object);

            var result = controller.Details(null) as HttpStatusCodeResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        [Test]
        public void TestIndex()
        {
            var context = new Mock<Context>();
            var rates = new List<Rate> { new Rate { CurrencyCode = "EUR" }, new Rate { CurrencyCode = "USD" } };
            var contextRates = DbSetMockBuilder.Build(rates.AsQueryable());

            context.Setup(c => c.Rates).Returns(contextRates.Object);

            var controller = new RatesController(context.Object);
            var result = controller.Index();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(rates));
        }
    }
}
