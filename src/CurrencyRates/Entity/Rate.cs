using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyRates.Entity
{
    public class Rate
    {
        public int Id { get; set; }
        [Column(TypeName = "Date"), Index("Date_CurrencyCode", 1, IsUnique = true)]
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public int Multiplier { get; set; }

        [Index("Date_CurrencyCode", 2, IsUnique = true), Required]
        public string CurrencyCode { get; set; }
        public int FileId { get; set; }

        public Currency Currency { get; set; }
        public File File { get; set; }
    }
}
