using System.Web.Mvc;

namespace CurrencyRates.Web.Controllers
{
    public class HomeController : Controller
    {
        public RedirectToRouteResult Index()
        {
            return RedirectToAction("Index", "Rates");
        }      
    }
}
