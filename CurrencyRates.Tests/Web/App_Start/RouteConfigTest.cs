using CurrencyRates.Web;
using Moq;
using NUnit.Framework;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CurrencyRates.Tests.Web.App_Start
{
    [TestFixture]
    class RouteConfigTest
    {
        [Test]
        public void TestRouteToEmbeddedResources()
        {
            var mockContext = new Mock<HttpContextBase>();

            mockContext
                .Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/handler.axd");

            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(mockContext.Object);

            Assert.That(routeData, Is.Not.Null);
            Assert.That(routeData.RouteHandler, Is.InstanceOf(typeof(StopRoutingHandler)));
        }

        [Test]
        public void TestRouteToHomepage()
        {
            var mockContext = new Mock<HttpContextBase>();

            mockContext
                .Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/");

            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(mockContext.Object);

            Assert.That(routeData, Is.Not.Null);
            Assert.That(routeData.Values["controller"], Is.EqualTo("Home"));
            Assert.That(routeData.Values["action"], Is.EqualTo("Index"));
            Assert.That(routeData.Values["id"], Is.EqualTo(UrlParameter.Optional));
        }
    }
}
