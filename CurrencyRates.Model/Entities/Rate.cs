namespace CurrencyRates.Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Rate
    {
        public virtual Currency Currency { get; set; }

        [Index("Date_CurrencyCode", 2, IsUnique = true), Required]
        public string CurrencyCode { get; set; }

        [Column(TypeName = "Date"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}"), Index("Date_CurrencyCode", 1, IsUnique = true)]
        public DateTime Date { get; set; }

        public virtual File File { get; set; }

        public int FileId { get; set; }

        public int Id { get; set; }

        public int Multiplier { get; set; }

        public decimal Value { get; set; }
    }
}
