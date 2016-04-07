namespace CurrencyRates.Tests.Base.Extension
{
    using System;

    using CurrencyRates.Base.Extension;

    using NUnit.Framework;

    [TestFixture]
    internal class StringUtilsTest
    {
        private static readonly object[] TruncateTestCases =
        {
            new object[] { string.Empty, 1, string.Empty },
            new object[] { "Example", 4, "E..." },
            new object[] { "Example", 6, "Exa..." },
            new object[] { "Example", 7, "Example" },
            new object[] { "Example", 8, "Example" }
        };

        [TestCaseSource(nameof(TruncateTestCases))]
        public void TestTruncate(string value, int maxLength, string truncated)
        {
            Assert.That(StringUtils.Truncate(value, maxLength), Is.EqualTo(truncated));
        }      

        [Test]
        public void TestTruncateThrowsException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => StringUtils.Truncate("Example", 3));

            Assert.That(exception.ParamName, Is.EqualTo("maxLength"));
            Assert.That(exception.Message, Does.Contain("maxLength must be at least 4"));
        }
    }
}
