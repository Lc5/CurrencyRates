using System;
using System.Xml.Linq;

namespace CurrencyRates.Service.NbpCurrencyRates.Entity
{
    class CurrencyRate
    {
        const string XmlCurrencyName = "nazwa_waluty";
        const string XmlMultiplier = "przelicznik";
        const string XmlCurrencyCode = "kod_waluty";
        const string XmlAverageValue = "kurs_sredni";

        public string CurrencyName { get; private set; }
        public int Multiplier { get; private set; }
        public string CurrencyCode { get; private set; }
        public decimal AverageValue { get; private set; }

        private CurrencyRate() {}

        public static CurrencyRate BuildFromXml(string xmlString)
        {
            var xmlRoot = XDocument.Parse(xmlString).Root;

            var currencyName = xmlRoot.Element(XmlCurrencyName).Value;
            var multiplier = Int32.Parse(xmlRoot.Element(XmlMultiplier).Value);
            var currencyCode = xmlRoot.Element(XmlCurrencyCode).Value;
            var averageValue = decimal.Parse(xmlRoot.Element(XmlAverageValue).Value);

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
