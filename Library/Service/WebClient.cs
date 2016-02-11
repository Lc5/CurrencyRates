namespace CurrencyRates.Library.Service
{
    public class WebClient : IWebClient
    {
        System.Net.WebClient Client;

        public WebClient(System.Net.WebClient client)
        {
            Client = client;
        }    

        public string DownloadString(string address)
        {
            return Client.DownloadString(address);
        }
    }
}