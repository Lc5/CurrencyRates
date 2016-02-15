using NUnit.Framework;

namespace CurrencyRates.Model.Entity.Comparer
{
    [TestFixture]
    class CurrencyComparerTest
    {
        [Test]
        public void TestEquals()
        {
            var first = new Currency() { Code = "PLN" };
            var second = new Currency() { Code = "PLN" };     
            var comparer = new CurrencyComparer();

            Assert.True(comparer.Equals(first, second));
        }

        [Test]
        public void TestEqualsNotEqual()
        {
            var first = new Currency() { Code = "PLN" };
            var second = new Currency() { Code = "CHF" };
            var comparer = new CurrencyComparer();

            Assert.False(comparer.Equals(first, second));
        }

        [Test]
        public void TestGetHashCode()
        {
            var currency = new Currency() { Code = "PLN" };
            var comparer = new CurrencyComparer();
            
            Assert.AreEqual(currency.Code.GetHashCode(), comparer.GetHashCode(currency));
        }
    }
}
