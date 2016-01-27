using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurrencyRates.Entity
{ 
    public class Currency
    {
        [Key, MaxLength(3), MinLength(3)]
        public string Code { get; set; }
        [Required, MaxLength(128)]
        public string Name { get; set; }

        public ICollection<Rate> Rates { get; set; }
    }
}
