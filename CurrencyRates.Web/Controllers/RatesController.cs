using System.Linq;
using System.Net;
using System.Web.Mvc;
using CurrencyRates.Model;
using CurrencyRates.Model.Query;

namespace CurrencyRates.Web.Controllers
{
    public class RatesController : Controller
    {
        private readonly Context Context;

        public RatesController(Context context)
        {
            Context = context;
        }

        public ViewResult Index()
        {
            var rates = Context.Rates.FindLatest();

            return View(rates.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var rate = Context.Rates.Find(id);

            if (rate == null)
            {
                return HttpNotFound();
            }

            return View(rate);
        }
     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
