using System;
using System.Collections.ObjectModel;

namespace CurrencyRates.Service.Nbp.Entity
{
    class CurrencyRateCollection : Collection<CurrencyRate>
    {
        public string TableNumber { get; set; }
        public DateTime PublicationDate { get; set; }

        public CurrencyRateCollection(string tableNumber, DateTime publicationDate)
        {
            TableNumber = tableNumber;
            PublicationDate = publicationDate;
        }
    }
}
