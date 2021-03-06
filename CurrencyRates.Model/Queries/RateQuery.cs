﻿namespace CurrencyRates.Model.Queries
{
    using System.Data.Entity;
    using System.Linq;

    using CurrencyRates.Model.Entities;

    public static class RateQuery
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
