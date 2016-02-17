﻿using CurrencyRates.NbpCurrencyRates.Service.Entity;
using NUnit.Framework;

namespace CurrencyRates.Tests.NbpCurrencyRates.Service.Entity
{
    [TestFixture]
    class CurrencyRateTest
    {
        [Test]
        public void TestBuildFromXml()
        {
            const string xml = @"
                <pozycja>
                    <nazwa_waluty>dolar amerykański</nazwa_waluty>
                    <przelicznik>1</przelicznik>
                    <kod_waluty>USD</kod_waluty>
                    <kurs_sredni>4,0917</kurs_sredni>
                </pozycja>
            ";

            var currencyRate = CurrencyRate.BuildFromXml(xml);

            Assert.That(currencyRate.CurrencyName, Is.EqualTo("dolar amerykański"));
            Assert.That(currencyRate.Multiplier, Is.EqualTo(1));
            Assert.That(currencyRate.CurrencyCode, Is.EqualTo("USD"));
            Assert.That(currencyRate.AverageValue, Is.EqualTo(4.0917));
        }
    }
}
