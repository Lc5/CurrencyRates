namespace CurrencyRates.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using CurrencyRates.Model;
    using CurrencyRates.Model.Queries;

    public class RatesController : Controller
    {
        private readonly Context context;

        public RatesController(Context context)
        {
            this.context = context;
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var rate = this.context.Rates.Find(id);

            if (rate == null)
            {
                return this.HttpNotFound();
            }

            return this.View(rate);
        }

        public ViewResult Index()
        {
            var rates = this.context.Rates.FindLatest();

            return this.View(rates.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
