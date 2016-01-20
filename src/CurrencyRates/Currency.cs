using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurrencyRates
{ 
    class Currency
    {
        [Key]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Rate> Rates { get; set; }
    }
}
