namespace CurrencyRates.Console.Presentation
{
    using System.Collections.Generic;
    using System.Text;

    using CurrencyRates.Base.Extension;
    using CurrencyRates.Model.Entity;

    public static class RateRenderer
    {
        public static string Render(IEnumerable<Rate> rates)
        {
            var separator = new string('-', 79) + "\n";
            const string Format = "| {0, -10} | {1, -40} | {2, 11} | {3, 5} |\n";
            var output = new StringBuilder();

            output.Append(separator);
            output.AppendFormat(Format, "Date", "Currency", "Value", "Multi");
            output.Append(separator);

            foreach (var rate in rates)
            {
                output.AppendFormat(
                    Format, 
                    rate.Date.ToString("dd-MM-yyyy"), 
                    rate.CurrencyCode + " " + StringUtils.Truncate(rate.Currency.Name, 36), 
                    rate.Value + " PLN", 
                    rate.Multiplier);
            }

            output.Append(separator);

            return output.ToString();
        }
    }
}
