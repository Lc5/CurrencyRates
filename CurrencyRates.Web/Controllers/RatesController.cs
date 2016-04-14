namespace CurrencyRates.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using CurrencyRates.Model.Entities;
    using CurrencyRates.Web.CurrencyRatesService;

    public class RatesController : Controller
    {
        private readonly ICurrencyRatesService currencyRatesService;

        public RatesController(ICurrencyRatesService currencyRatesService)
        {
            this.currencyRatesService = currencyRatesService;
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var rate = this.currencyRatesService.Find(id.GetValueOrDefault());

            if (rate == null)
            {
                return this.HttpNotFound();
            }

            return this.View(rate);
        }

        public ViewResult Index()
        {
            var rates = (IEnumerable<Rate>)this.currencyRatesService.FindLatest();

            return this.View(rates.ToList());
        }
    }
}
