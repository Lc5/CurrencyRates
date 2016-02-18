using System.Web.Mvc;

namespace CurrencyRates.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }      
    }
}