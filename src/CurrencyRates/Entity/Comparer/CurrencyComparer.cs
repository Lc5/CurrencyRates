using System.Collections.Generic;

namespace CurrencyRates.Entity.Comparer
{
    public class CurrencyComparer : IEqualityComparer<Currency>
    {
        public bool Equals(Currency first, Currency second)
        {
            return first.Code == second.Code;
        }

        public int GetHashCode(Currency obj)
        {
            return obj.Code.GetHashCode();
        }
    }
}
