using System;
using System.Xml.Linq;

namespace CurrencyRates.Service.Nbp.Entity
{
    class CurrencyRate
    {
        public string CurrencyName { get; set; }
        public int Multiplier { get; set; }
        public string CurrencyCode { get; set; }
        public decimal AverageValue { get; set; }

        public static CurrencyRate buildFromXml(string xmlString)
        {
            var xmlRoot = XDocument.Parse(xmlString).Root;

            var currencyName = xmlRoot.Element("nazwa_waluty").Value;
            var multiplier = Int32.Parse(xmlRoot.Element("przelicznik").Value);
            var currencyCode = xmlRoot.Element("kod_waluty").Value;
            var averageValue = decimal.Parse(xmlRoot.Element("kurs_sredni").Value);

            var currencyRate = new CurrencyRate
            {
                CurrencyName = currencyName,
                Multiplier = multiplier,
                CurrencyCode = currencyCode,
                AverageValue = averageValue
            };

            return currencyRate;
        }
    }
}
