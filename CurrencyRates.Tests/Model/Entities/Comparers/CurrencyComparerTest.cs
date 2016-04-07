namespace CurrencyRates.Tests.Model.Entities.Comparers
{
    using CurrencyRates.Model.Entities;
    using CurrencyRates.Model.Entities.Comparers;

    using NUnit.Framework;

    [TestFixture]
    public class CurrencyComparerTest
    {
        [Test]
        public void TestEquals()
        {
            var first = new Currency { Code = "PLN" };
            var second = new Currency { Code = "PLN" };
            var comparer = new CurrencyComparer();

            Assert.That(comparer.Equals(first, second));
        }

        [Test]
        public void TestEqualsNotEqual()
        {
            var first = new Currency { Code = "PLN" };
            var second = new Currency { Code = "CHF" };
            var comparer = new CurrencyComparer();

            Assert.That(!comparer.Equals(first, second));
        }

        [Test]
        public void TestGetHashCode()
        {
            var currency = new Currency { Code = "PLN" };
            var comparer = new CurrencyComparer();

            Assert.That(currency.Code.GetHashCode(), Is.EqualTo(comparer.GetHashCode(currency)));
        }
    }
}
