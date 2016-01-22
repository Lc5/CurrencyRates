using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyRates.Entity
{
    class Rate
    {
        public int Id { get; set; }
        //@todo unique?
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public int Multiplier { get; set; }

        [Required]
        public string CurrencyCode { get; set; }
        public int FileId { get; set; }

        public Currency Currency { get; set; }
        public File File { get; set; }
    }
}
