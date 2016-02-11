namespace CurrencyRates.Library.Service
{
    public interface IWebClient
    {
        string DownloadString(string address);
    }
}
