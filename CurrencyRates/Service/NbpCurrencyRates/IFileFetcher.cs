using CurrencyRates.Service.NbpCurrencyRates.Entity;
using System.Collections.Generic;

namespace CurrencyRates.Service.NbpCurrencyRates
{
    public interface IFileFetcher
    {
        IEnumerable<File> FetchAllFilesExcept(IEnumerable<string> existingFilenames);
    }
}
