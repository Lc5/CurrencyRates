namespace CurrencyRates.WebService
{
    using System.Collections.Generic;
    using System.Data.Entity;

    using CurrencyRates.Model;
    using CurrencyRates.Model.Entities;
    using CurrencyRates.Model.Queries;

    public class CurrencyRatesService : ICurrencyRatesService
    {
        private readonly Context context;

        public CurrencyRatesService(Context context)
        {
            this.context = context;
            this.context.Configuration.ProxyCreationEnabled = false;
        }

        public Rate Find(int id)
        {
            return this.context.Rates.Find(id);
        }

        public IEnumerable<Rate> FindLatest()
        {           
            return this.context.Rates.FindLatest().Include(r => r.Currency);
        }
    }
}
