namespace CurrencyRates.Service.Nbp.Entity
{
    class CurrencyRate
    {
        public string CurrencyName { get; set; }
        public int Multiplier { get; set; }
        public string CurrencyCode { get; set; }
        public decimal AverageValue { get; set; }
    }
}
