﻿namespace CurrencyRates.NbpCurrencyRates.Services.Entities
{
    using System.Xml.Linq;

    using CurrencyRates.NbpCurrencyRates.Extensions;

    public class CurrencyRate
    {
        private CurrencyRate()
        {
        }

        public decimal AverageValue { get; private set; }

        public string CurrencyCode { get; private set; }

        public string CurrencyName { get; private set; }

        public int Multiplier { get; private set; }

        public static CurrencyRate BuildFromXml(string xmlString)
        {
            var xml = XDocument.Parse(xmlString);
            xml.Validate(GetXsd());
            var xmlRoot = xml.Root;

            var currencyRate = new CurrencyRate
            {
                CurrencyName = xmlRoot.Element(NbpXml.CurrencyName).Value, 
                Multiplier = int.Parse(xmlRoot.Element(NbpXml.Multiplier).Value), 
                CurrencyCode = xmlRoot.Element(NbpXml.CurrencyCode).Value, 
                AverageValue = decimal.Parse(xmlRoot.Element(NbpXml.AverageValue).Value)
            };

            return currencyRate;
        }

        private static string GetXsd()
        {
            return @"
                <xs:schema xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
                    <xs:element name=""" + NbpXml.Position + @""">
                        <xs:complexType>
                            <xs:sequence>
                                <xs:element type=""xs:string"" name=""" + NbpXml.CurrencyName + @"""/>
                                <xs:element type=""xs:positiveInteger"" name=""" + NbpXml.Multiplier + @"""/>
                                <xs:element type=""xs:string"" name=""" + NbpXml.CurrencyCode + @"""/>
                                <xs:element type=""xs:string"" name=""" + NbpXml.AverageValue + @"""/>
                            </xs:sequence>
                        </xs:complexType>
                    </xs:element>
                </xs:schema>";
        }
    }
}
