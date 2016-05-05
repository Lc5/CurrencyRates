namespace CurrencyRates.Tests.Base.Extensions
{
    using System;

    using CurrencyRates.Base.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public class StringUtilsTest
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
            Assert.That(
                () => StringUtils.Truncate("Example", 3),
                Throws
                    .TypeOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName").EqualTo("maxLength")
                    .With.Property("Message").Contain("maxLength must be at least 4")

            );
        }
    }
}
