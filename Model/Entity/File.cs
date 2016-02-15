using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyRates.Model.Entity
{
    public class File
    {
        public int Id { get; set; }
        [Index(IsUnique = true), MaxLength(15),  MinLength(15), Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        public bool Processed { get; set; }
    }
}
