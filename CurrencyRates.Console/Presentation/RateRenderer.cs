using CurrencyRates.Common.Extension;
using CurrencyRates.Model.Entity;
using System.Collections.Generic;
using System.Text;

namespace CurrencyRates.Console.Presentation
{
    public class RateRenderer
    {
        public static string Render(IEnumerable<Rate> rates)
        {
            var separator = new string('-', 79) + "\n";
            const string format = "| {0, -10} | {1, -40} | {2, 11} | {3, 5} |\n";
            var output = new StringBuilder();

            output.Append(separator);
            output.AppendFormat(format, "Date", "Currency", "Value", "Multi");
            output.Append(separator);

            foreach (var rate in rates)
            {
                output.AppendFormat(format, rate.Date.ToString("dd-MM-yyyy"), rate.CurrencyCode + " " + StringUtils.Truncate(rate.Currency.Name, 36), rate.Value + " PLN", rate.Multiplier);
            }

            output.Append(separator);

            return output.ToString();
        }
    }
}
