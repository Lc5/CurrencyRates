﻿using System;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace CurrencyRates.Service.NbpCurrencyRates.Entity.Collection
{
    class CurrencyRateCollection : Collection<CurrencyRate>
    {
        public string TableNumber { get; set; }
        public DateTime PublicationDate { get; set; }

        public CurrencyRateCollection(string tableNumber, DateTime publicationDate)
        {
            TableNumber = tableNumber;
            PublicationDate = publicationDate;
        }

        public static CurrencyRateCollection BuildFromXml(string xmlString)
        {
            var xml = XDocument.Parse(xmlString);

            var tableNumber = xml.Root.Element("numer_tabeli").Value;
            var publicationDate = DateTime.Parse(xml.Root.Element("data_publikacji").Value);

            var collection = new CurrencyRateCollection(tableNumber, publicationDate);

            foreach (var xmlRate in xml.Root.Elements("pozycja"))
            {
                var currencyRate = CurrencyRate.BuildFromXml(xmlRate.ToString());
                collection.Add(currencyRate);
            }

            return collection;
        }
    }
}