using System;

namespace CurrencyRates
{
    class Rate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public int Multiplier { get; set; }

        public Currency Currency { get; set; }
        public File File { get; set; }
    }
}
