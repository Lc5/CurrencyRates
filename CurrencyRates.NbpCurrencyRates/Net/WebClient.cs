﻿namespace CurrencyRates.NbpCurrencyRates.Net
{
    public class WebClient : IWebClient
    {
        readonly System.Net.WebClient Client;

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