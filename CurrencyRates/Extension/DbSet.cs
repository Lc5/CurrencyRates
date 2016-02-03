using CurrencyRates.Entity;
using System.Data.Entity;
using System.Linq;

namespace CurrencyRates.Extension
{
    static class DbSet
    {
        public static IOrderedQueryable<Rate> FindLatest(this DbSet<Rate> rates)
        {
            return rates
                .GroupBy(r => r.CurrencyCode)
                .Select(g => g.OrderByDescending(r => r.Date))
                .Select(g => g.FirstOrDefault())
                .OrderBy(r => r.CurrencyCode);
        }
    }
}
