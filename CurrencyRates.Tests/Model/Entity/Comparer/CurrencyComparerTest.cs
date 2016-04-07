namespace CurrencyRates.Tests.Model.Entity.Comparer
{
    using CurrencyRates.Model.Entity;
    using CurrencyRates.Model.Entity.Comparer;

    using NUnit.Framework;

    [TestFixture]
    internal class CurrencyComparerTest
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
