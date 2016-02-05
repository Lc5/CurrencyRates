namespace CurrencyRates.Extension
{
    public static class StringUtils
    {
        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            if (maxLength < 4)
            {
                throw new System.ArgumentOutOfRangeException("maxLength", maxLength, "maxLength must be at least 4");
            }
         
            return value.Length <= maxLength ? value : value.Substring(0, maxLength - 3) + "...";
        }
    }
}
