namespace CurrencyRates.Tests.Web.Controllers
{
    using CurrencyRates.Web.Controllers;

    using NUnit.Framework;

    [TestFixture]
    public class HomeControllerTest
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
