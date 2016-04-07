namespace CurrencyRates.Tests.NbpCurrencyRates.Service.Entity.Collection
{
    using System;

    using CurrencyRates.NbpCurrencyRates.Service.Entity.Collection;

    using NUnit.Framework;

    [TestFixture]
    internal class CurrencyRatesCollectionTest
    {
        [Test]
        public void TestBuildFromXml()
        {
            const string Xml = @"
                <tabela_kursow typ=""A"" uid=""16a011"">
                    <numer_tabeli>011/A/NBP/2016</numer_tabeli>
                    <data_publikacji>2016-01-19</data_publikacji>
                    <pozycja>
                        <nazwa_waluty>dolar amerykański</nazwa_waluty>
                        <przelicznik>1</przelicznik>
                        <kod_waluty>USD</kod_waluty>
                        <kurs_sredni>4,0917</kurs_sredni>
                    </pozycja>
                    <pozycja>
                        <nazwa_waluty>dolar australijski</nazwa_waluty>
                        <przelicznik>1</przelicznik>
                        <kod_waluty>AUD</kod_waluty>
                        <kurs_sredni>2,8331</kurs_sredni>
                    </pozycja>
                </tabela_kursow>
            ";

            var currencyRateCollection = CurrencyRateCollection.BuildFromXml(Xml);

            Assert.That(currencyRateCollection.TableNumber, Is.EqualTo("011/A/NBP/2016"));
            Assert.That(currencyRateCollection.PublicationDate, Is.EqualTo(DateTime.Parse("2016-01-19")));
            Assert.That(currencyRateCollection, Has.Count.EqualTo(2));
        }
    }
}
