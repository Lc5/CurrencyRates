# CurrencyRates
.NET console application for fetching and displaying currency rates from NBP webservice. The app was developed as a research project with aim to learn C#, .NET and Entity Framework.

## Description
The app fetches all the average currency rates for the current year from NBP webservice, saves them to local DB and displays the newest rates for each currency. Each subsequent run will fetch and process only new rates.

More information: (http://www.nbp.pl/home.aspx?f=/kursy/instrukcja_pobierania_kursow_walut.html)

## Usage
The project can be compiled and run using Visual Studio 2015. To run the program use the following command:

```
CurrencyRates.exe [action]
```

There are three actions available:
- ```Fetch``` - fetches the xml files from the webservice and saves them into DB
- ```Process``` - processes unprocessed files from DB and save rates into DB
- ```Show``` - displays the newest rates for each currency

If no argument is given ```Fetch```, ```Process``` and ```Show``` are run one after another.

