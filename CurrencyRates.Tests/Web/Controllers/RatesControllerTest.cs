﻿using CurrencyRates.Model;
using CurrencyRates.Model.Entity;
using CurrencyRates.Web.Controllers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CurrencyRates.Tests.Web.Controllers
{
    [TestFixture]
    class RatesControllerTest
    {
        [Test]
        public void TestIndex()
        {
            var context = new Mock<Context>();
            var rates = new List<Rate>
            {
                new Rate() { CurrencyCode = "EUR" },
                new Rate() { CurrencyCode = "USD" }
            };
            var contextRates = TestUtils.BuildDbSetMock(rates.AsQueryable());
  
            context.Setup(c => c.Rates).Returns(contextRates.Object);

            var controller = new RatesController(context.Object);
            var result = controller.Index();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(rates));
        }

        [Test]
        public void TestDetails()
        {
            var context = new Mock<Context>();
            var rates = new List<Rate>
            {
                new Rate() { Id = 123, CurrencyCode = "EUR" }
            };
            var contextRates = TestUtils.BuildDbSetMock(rates.AsQueryable());

            contextRates.Setup(cr => cr
                .Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => rates.FirstOrDefault(r => r.Id == (int) ids[0]));

            context.Setup(c => c.Rates).Returns(contextRates.Object);

            var controller = new RatesController(context.Object);
            var result = controller.Details(123) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(rates.First()));
        }

        [Test]
        public void TestDetailsWithNullId()
        {
            var context = new Mock<Context>();
            var controller = new RatesController(context.Object);

            var result = controller.Details(null) as HttpStatusCodeResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int) HttpStatusCode.BadRequest));
        }

        [Test]
        public void TestDetailsWithNotExistingId()
        {
            var context = new Mock<Context>();      
            var contextRates = TestUtils.BuildDbSetMock(Enumerable.Empty<Rate>().AsQueryable());

            context.Setup(c => c.Rates).Returns(contextRates.Object);

            var controller = new RatesController(context.Object);
            var result = controller.Details(123);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<HttpNotFoundResult>());
        }
    }
}
