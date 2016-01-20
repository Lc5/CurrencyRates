namespace CurrencyRates.Service.Nbp
{
    class Nbp
    {
        string Url;

        public Nbp(string url = "http://www.nbp.pl/kursy/xml/")
        {
            Url = url;
        }
    }
}
