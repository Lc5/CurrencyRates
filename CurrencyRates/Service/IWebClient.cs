namespace CurrencyRates.Service
{
    public interface IWebClient
    {
        string DownloadString(string address);
        void Dispose();
    }
}
