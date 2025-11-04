[README po polsku](README.md)

# 1000 Words
An app for learning the 1000 most common English words.

https://1000slowek.netlify.app/

## Features
- learning mode  
- quiz/testing mode  
- automatic dark/light theme  
- progress saved in browser storage  

## Branches
- **develop**: current changes, automated build and tests  
- **main**: automatic deploy to Netlify  

## Technologies used
- .NET 8 Blazor WebAssembly  
- Bootstrap 5.3  
- Testing: xUnit, Moq, bUnit  

## Test coverage report

Requires PowerShell and the report generator tool:

```
dotnet tool install -g dotnet-reportgenerator-globaltool
```

Usage:
```
cd src
pwsh Test-Coverage.ps1
```
