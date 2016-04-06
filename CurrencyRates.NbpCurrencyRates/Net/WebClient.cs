namespace CurrencyRates.NbpCurrencyRates.Net
{
    using System;

    public class WebClient : IWebClient, IDisposable
    {
        private readonly System.Net.WebClient client;

        public WebClient(System.Net.WebClient client)
        {
            this.client = client;
        }

        public void Dispose()
        {
            this.client.Dispose();
        }

        public string DownloadString(string address)
        {
            return this.client.DownloadString(address);
        }
    }
}
