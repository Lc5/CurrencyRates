using System.Collections.Generic;

namespace CurrencyRates.Model.Entity.Comparer
{
    public class CurrencyComparer : IEqualityComparer<Currency>
    {
        public bool Equals(Currency first, Currency second)
        {
            return first.Code == second.Code;
        }

        public int GetHashCode(Currency currency)
        {
            return currency.Code.GetHashCode();
        }
    }
}
