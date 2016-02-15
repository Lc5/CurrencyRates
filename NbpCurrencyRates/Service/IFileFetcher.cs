using CurrencyRates.NbpCurrencyRates.Service.Entity;
using System.Collections.Generic;

namespace CurrencyRates.NbpCurrencyRates.Service
{
    public interface IFileFetcher
    {
        IEnumerable<File> FetchAllFilesExcept(IEnumerable<string> existingFilenames);
    }
}
