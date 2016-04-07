namespace CurrencyRates.Model.Entities.Comparers
{
    using System.Collections.Generic;

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
