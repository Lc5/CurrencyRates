﻿namespace CurrencyRates.NbpCurrencyRates.Services.Entities.Collections
{
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Linq;

    using CurrencyRates.NbpCurrencyRates.Extensions;

    public class CurrencyRateCollection : Collection<CurrencyRate>
    {
        private CurrencyRateCollection()
        {
        }

        public DateTime PublicationDate { get; private set; }

        public string TableNumber { get; private set; }

        public static CurrencyRateCollection BuildFromXml(string xmlString)
        {
            var xml = XDocument.Parse(xmlString);
            xml.Validate(GetXsd());
            var xmlRoot = xml.Root;

            var collection = new CurrencyRateCollection
            {
                TableNumber = xmlRoot.Element(NbpXml.TableNumber).Value, 
                PublicationDate = DateTime.Parse(xmlRoot.Element(NbpXml.PublicationDate).Value)
            };

            foreach (var xmlRate in xmlRoot.Elements(NbpXml.Position))
            {
                var currencyRate = CurrencyRate.BuildFromXml(xmlRate.ToString());
                collection.Add(currencyRate);
            }

            return collection;
        }

        private static string GetXsd()
        {
            return @"
                <xs:schema xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
                    <xs:element name=""" + NbpXml.RateTable + @""">
                        <xs:complexType>
                            <xs:sequence>
                                <xs:element type=""xs:string"" name=""" + NbpXml.TableNumber + @"""/>
                                <xs:element type=""xs:date"" name=""" + NbpXml.PublicationDate + @"""/>
                                <xs:element name=""" + NbpXml.Position + @""" maxOccurs=""unbounded"" />                                           
                            </xs:sequence>
                            <xs:attribute type=""xs:string"" name=""" + NbpXml.Type + @"""/>
                            <xs:attribute type=""xs:string"" name=""" + NbpXml.Uid + @"""/>
                        </xs:complexType>
                    </xs:element>
                </xs:schema>";
        }
    }
}
