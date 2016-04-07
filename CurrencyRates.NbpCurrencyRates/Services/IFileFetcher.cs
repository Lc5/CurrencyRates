namespace CurrencyRates.NbpCurrencyRates.Services
{
    using System.Collections.Generic;

    using CurrencyRates.NbpCurrencyRates.Services.Entities;

    public interface IFileFetcher
    {
        IEnumerable<File> FetchAllFilesExcept(IEnumerable<string> existingFilenames);
    }
}
