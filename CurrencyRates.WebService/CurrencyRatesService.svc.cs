namespace CurrencyRates.WebService
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using CurrencyRates.Model;
    using CurrencyRates.Model.Entities;
    using CurrencyRates.Model.Queries;

    public class CurrencyRatesService : ICurrencyRatesService
    {
        private readonly Context context;

        public CurrencyRatesService(Context context)
        {
            this.context = context;
        }

        public Rate Find(int id)
        {
            return this.context
                .Rates
                .Include(r => r.File)
                .SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Rate> FindLatest()
        {           
            return this.context
                .Rates
                .FindLatest()
                .Include(r => r.Currency);
        }
    }
}
