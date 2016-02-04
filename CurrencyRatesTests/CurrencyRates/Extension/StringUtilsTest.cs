using NUnit.Framework;

namespace CurrencyRates.Extension
{
    [TestFixture]
    class StringUtilsTest
    {
        [TestCaseSource("TruncateTestCases")]
        public void TestTruncate(string value, int maxLength, string truncated)
        {
            Assert.AreEqual(truncated, value.Truncate(maxLength));
        }

        static object[] TruncateTestCases =
        {
            new object[] {"", 1, ""},
            new object[] {"Example", 4, "E..."},
            new object[] {"Example", 6, "Exa..."},
            new object[] {"Example", 7, "Example"},
            new object[] {"Example", 8, "Example"}
        };

        [Test]
        public void TestTruncateThrowsException()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(
                () => "Example".Truncate(3)    
            );
        }
    }
}
