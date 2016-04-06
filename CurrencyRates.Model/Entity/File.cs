namespace CurrencyRates.Model.Entity
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class File
    {
        [Required]
        public string Content { get; set; }

        public int Id { get; set; }

        [DisplayName("Filename"), Index(IsUnique = true), MaxLength(15), MinLength(15), Required]
        public string Name { get; set; }

        public bool Processed { get; set; }
    }
}
