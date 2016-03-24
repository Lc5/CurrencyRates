using CurrencyRates.Base.Extension;
using NUnit.Framework;

namespace CurrencyRates.Tests.Common.Extension
{
    [TestFixture]
    class StringUtilsTest
    {
        [TestCaseSource(nameof(TruncateTestCases))]
        public void TestTruncate(string value, int maxLength, string truncated)
        {
            Assert.That(StringUtils.Truncate(value, maxLength), Is.EqualTo(truncated));
        }

        static readonly object[] TruncateTestCases =
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

            Assert.That(exception.ParamName, Is.EqualTo("maxLength"));
            Assert.That(exception.Message, Does.Contain("maxLength must be at least 4"));
        }
    }
}
