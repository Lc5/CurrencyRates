namespace CurrencyRates.Model.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class Currency
    {
        [DataMember, Key, MaxLength(3), MinLength(3)]
        public string Code { get; set; }

        [DataMember, MaxLength(128), Required]
        public string Name { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
    }
}
