using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyRates.Entity
{
    class File
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        public bool Processed { get; set; }
    }
}
