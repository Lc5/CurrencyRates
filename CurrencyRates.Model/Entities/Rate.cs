namespace CurrencyRates.Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [DataContract]
    public class Rate
    {
        [DataMember]
        public virtual Currency Currency { get; set; }

        [Index("Date_CurrencyCode", 2, IsUnique = true), Required]
        public string CurrencyCode { get; set; }

        [DataMember, Column(TypeName = "Date"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}"), Index("Date_CurrencyCode", 1, IsUnique = true)]
        public DateTime Date { get; set; }

        [DataMember]
        public virtual File File { get; set; }

        public int FileId { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Multiplier { get; set; }

        [DataMember]
        public decimal Value { get; set; }
    }
}
