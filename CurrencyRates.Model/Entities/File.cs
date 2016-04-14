namespace CurrencyRates.Model.Entities
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [DataContract]
    public class File
    {
        [Required]
        public string Content { get; set; }

        public int Id { get; set; }

        [DataMember, DisplayName("Filename"), Index(IsUnique = true), MaxLength(15), MinLength(15), Required]
        public string Name { get; set; }

        public bool Processed { get; set; }
    }
}
