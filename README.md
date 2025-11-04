[README English version](README.en.md)

# 1000 słówek
Aplikacja do nauki 1000 najpopularniejszych słówek języka angielskiego.

https://1000slowek.netlify.app/

## Funkcje
- tryb nauki słówek
- tryb sprawdzania
- automatyczny wygląd ciemny/jasny
- postępy zapisywane w pamięci przeglądarki

## Branche
- develop: aktualne zmiany, automatyczny build i testy
- main: automatyczny deploy na Netlify

## Użyte technologie
- .NET 8 Blazor Web Assembly
- Bootstrap 5.3
- testowanie xUnit, Moq, bUnit

## Raport pokrycia kodu testami 

Wymagany PowerShell oraz narzędzie do generowania:

```
dotnet tool install -g dotnet-reportgenerator-globaltool
```

Użycie:
```
cd src
pwsh Test-Coverage.ps1
```
