namespace CurrencyRates.NbpCurrencyRates.Net
{
    public interface IWebClient
    {
        string DownloadString(string address);
    }
}
