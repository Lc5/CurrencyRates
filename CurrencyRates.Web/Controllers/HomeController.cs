namespace CurrencyRates.Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public RedirectToRouteResult Index()
        {
            return this.RedirectToAction("Index", "Rates");
        }
    }
}
