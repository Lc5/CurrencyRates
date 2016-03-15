using CurrencyRates.Web.Controllers;
using NUnit.Framework;

namespace CurrencyRates.Tests.Web.Controllers
{
    [TestFixture]
    class HomeControllerTest
    {
        [Test]
        public void TestIndex()
        {
            var controller = new HomeController();
            var result = controller.Index();

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Rates"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }
    }
}
