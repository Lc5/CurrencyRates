# Currency Rates
.NET console application, web application, WCF service and Windows service for fetching and displaying currency rates from NBP webservice. The app was developed as a research project with aim to learn C#, .NET, Entity Framework and ASP.NET MVC.

## Description
The app fetches all the average currency rates for the current year from NBP webservice, saves them to local DB and displays the newest rates for each currency. Each subsequent run will fetch and process only new rates.

More information: (http://www.nbp.pl/home.aspx?f=/kursy/instrukcja_pobierania_kursow_walut.html)

## Usage
The project can be compiled and run using Visual Studio 2015. 

### Console application
Startup project for console application is CurrencyRates.Console. To run the program use the following command:

```
CurrencyRates.exe [action]
```

There are three actions available:
- ```Fetch``` - fetches the xml files from the webservice and saves them into DB
- ```Process``` - processes unprocessed files from DB and save rates into DB
- ```Show``` - displays the newest rates for each currency

If no argument is given ```Fetch```, ```Process``` and ```Show``` are run one after another.

### Web application
The startup project for web application is CurrencyRates.Web. After running from Visual Studio web application shows the list of latest rates along with rates details.  

### WCF service
WCF service is configured to be hosted inside Web application. It acts as a data source for web application.

### Windows service
The startup project for Windows service is CurrencyRates.WindowsService. After compilation the service can be installed using the following command:

```
installutil.exe CurrencyRates.WindowsService.exe
```

The service can be started using "Start - Run - services.msc". Navigate to "CurrencyRates Scheduler" entry and click "Run". The service will synchronize currency rates every 60 seconds.
