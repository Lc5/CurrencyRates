namespace CurrencyRates.WebService
{
    using System.Collections.Generic;
    using System.ServiceModel;

    using CurrencyRates.Model.Entities;

    [ServiceContract]
    public interface ICurrencyRatesService
    {
        [OperationContract]
        Rate Find(int id);

        [OperationContract]
        IEnumerable<Rate> FindLatest();
    }
}
