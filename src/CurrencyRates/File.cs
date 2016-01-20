using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyRates
{
    class File
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TableNumber { get; set; }
        public DateTime PublicationDate { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
