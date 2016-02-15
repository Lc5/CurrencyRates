using CurrencyRates.Model.Entity;
using CurrencyRates.Common.Extension;
using System;
using System.Collections.Generic;

namespace CurrencyRates.Presentation
{
    public class RateRenderer
    {
        public static string Render(IEnumerable<Rate> rates)
        {
            var separator = new String('-', 79) + "\n";
            var format = "| {0, -10} | {1, -40} | {2, 11} | {3, 5} |\n";
            var output = "";

            output += separator;
            output += String.Format(format, "Date", "Currency", "Value", "Multi");
            output += separator;

            foreach (var rate in rates)
            {
                output += String.Format(format, rate.Date.ToString("dd-MM-yyyy"), rate.CurrencyCode + " " + StringUtils.Truncate(rate.Currency.Name, 36), rate.Value + " PLN", rate.Multiplier);
            }

            output += separator;

            return output;
        }
    }
}
