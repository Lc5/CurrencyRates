using System;
using System.Collections.Generic;

namespace CurrencyRates.Extension.Linq
{
    public static class Enumerable
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> existingKeys = new HashSet<TKey>();

            foreach (TSource element in source)
            {
                if (existingKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
