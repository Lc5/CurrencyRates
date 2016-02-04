using System.Xml.Linq;

namespace CurrencyRates.Service.NbpCurrencyRates.Entity
{
    public class CurrencyRate
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
            var xml = XDocument.Parse(xmlString).Root;

            var currencyRate = new CurrencyRate
            {
                CurrencyName = xml.Element(XmlCurrencyName).Value,
                Multiplier = int.Parse(xml.Element(XmlMultiplier).Value),
                CurrencyCode = xml.Element(XmlCurrencyCode).Value,
                AverageValue = decimal.Parse(xml.Element(XmlAverageValue).Value)
            };

            return currencyRate;
        }
    }
}
