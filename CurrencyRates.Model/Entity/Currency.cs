﻿namespace CurrencyRates.Model.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Currency
    {
        [Key, MaxLength(3), MinLength(3)]
        public string Code { get; set; }

        [MaxLength(128), Required]
        public string Name { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
    }
}
