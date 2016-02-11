using NUnit.Framework;

namespace CurrencyRates.Library.Extension
{
    [TestFixture]
    class StringUtilsTest
    {
        [TestCaseSource("TruncateTestCases")]
        public void TestTruncate(string value, int maxLength, string truncated)
        {
            Assert.AreEqual(truncated, StringUtils.Truncate(value, maxLength));
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
            var exception = Assert.Throws<System.ArgumentOutOfRangeException>(
                () => StringUtils.Truncate("Example", 3)    
            );

            Assert.AreEqual("maxLength", exception.ParamName);
            StringAssert.Contains("maxLength must be at least 4", exception.Message);
        }
    }
}
