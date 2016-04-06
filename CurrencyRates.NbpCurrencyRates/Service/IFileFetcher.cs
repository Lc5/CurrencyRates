namespace CurrencyRates.NbpCurrencyRates.Service
{
    using System.Collections.Generic;

    using CurrencyRates.NbpCurrencyRates.Service.Entity;

    public interface IFileFetcher
    {
        IEnumerable<File> FetchAllFilesExcept(IEnumerable<string> existingFilenames);
    }
}
