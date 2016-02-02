using System;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace CurrencyRates.Service.NbpCurrencyRates.Entity.Collection
{
    class CurrencyRateCollection : Collection<CurrencyRate>
    {
        const string XmlTableNumber = "numer_tabeli";
        const string XmlPublicationDate = "data_publikacji";
        const string XmlPosition = "pozycja";

        public string TableNumber { get; private set; }
        public DateTime PublicationDate { get; private set; }

        private CurrencyRateCollection() {}

        public static CurrencyRateCollection BuildFromXml(string xmlString)
        {
            var xml = XDocument.Parse(xmlString);

            var collection = new CurrencyRateCollection()
            {
                TableNumber = xml.Root.Element(XmlTableNumber).Value,
                PublicationDate = DateTime.Parse(xml.Root.Element(XmlPublicationDate).Value)
            };

            foreach (var xmlRate in xml.Root.Elements(XmlPosition))
            {
                var currencyRate = CurrencyRate.BuildFromXml(xmlRate.ToString());
                collection.Add(currencyRate);
            }

            return collection;
        }
    }
}
