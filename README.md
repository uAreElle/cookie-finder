# Most Active Cookie Finder

A command-line C# program that processes a cookie log file and returns the most active cookie(s) for a specific day.

## Problem

Given a CSV log file where each line contains a cookie and a timestamp, this program finds the cookie(s) that appear most frequently on a specified UTC date.

If multiple cookies share the highest count, all are printed (one per line).

## How to Run

This project targets [.NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0-preview).  
Make sure you have the .NET 9 SDK installed.

From the project root (`CookieFinder` directory):

```bash
dotnet run -- -f cookie_log.csv -d 2018-12-09
```
