using CurrencyRates.Library.Service.NbpCurrencyRates.Entity;
using System.Collections.Generic;

namespace CurrencyRates.Library.Service.NbpCurrencyRates
{
    public interface IFileFetcher
    {
        IEnumerable<File> FetchAllFilesExcept(IEnumerable<string> existingFilenames);
    }
}
