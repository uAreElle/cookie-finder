/*
Quantcast Take-Home Assignment: Most Active Cookie

Command line program that returns the most active cookie(s) for a specific day.

How to run program:
    dotnet run -- -f cookie_log.csv -d 2018-12-09
*/

using System.Globalization;

public class CookieFinder
{
    private static void Main(string[] args)
    {
        (string csvFileName, DateTime searchDate) = GetCommandLineArgs(args);
        string csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), csvFileName);

        List<string> cookies = GetMostActiveCookies(csvFileName, searchDate);
        PrintMostActiveCookies(cookies);
    }

    private static (string, DateTime) GetCommandLineArgs(string[] args)
    {
        string fileName = string.Empty;
        DateTime date = DateTime.MinValue;

        if (args.Length > 0)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                switch (arg)
                {
                    case "-f":
                        fileName = args[i + 1];
                        if (!fileName.EndsWith(".csv"))
                        {
                            LogError("Invalid .csv file name.");
                            Environment.Exit(1);
                        }
                        break;

                    case "-d":
                        if (!DateTime.TryParseExact(args[i + 1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                        {
                            LogError("Invalid date format.");
                            Environment.Exit(1);
                        }
                        date = parsedDate;
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid arguments.");
            Environment.Exit(1);
        }

        if (string.IsNullOrWhiteSpace(fileName) || date == DateTime.MinValue)
        {
            LogError("Unable to parse arguments. Please check -f and -d parameters.");
        }

        return (fileName, date);
    }

    private static List<string> GetMostActiveCookies(string csvFilePath, DateTime searchDate)
    {
        var mostActiveCookies = new List<string>();
        var cookieCounts = new Dictionary<string, int>();

        // TODO: Validate file path
        foreach (var logEntry in File.ReadLines(csvFilePath))
        {
            string[] splitEntry = logEntry.Split(',');

            // TODO: Validate log entry format
            // Example entry: "AtY0laUfhglK3lC7,2018-12-09T14:19:00+00:00"
            string cookie = splitEntry[0];
            if (!DateTime.TryParse(splitEntry[1], out DateTime cookieTime))
            {
                LogError($"Invalid timestamp in log entry: {logEntry} in file {csvFilePath}");
                Environment.Exit(1);
            }

            if (cookieTime.Date == searchDate)
            {
                // Add cookie to dictionary or increment count
                cookieCounts[cookie] = cookieCounts.GetValueOrDefault(cookie) + 1;
            }
            else if (cookieTime < searchDate)
            {
                break; // Short-circuit log parsing if past cookie day
            }
        }

        if (cookieCounts.Count == 0)
        {
            LogError($"No cookies found in logs {csvFilePath} for provided day {searchDate}.");
            Environment.Exit(1);
        }

        // 
        int maxCount = cookieCounts.Values.Max();
        foreach (var cookie in cookieCounts.Where(x => x.Value == maxCount))
        {
            mostActiveCookies.Add(cookie.Key);
        }

        return mostActiveCookies;
    }

    private static void PrintMostActiveCookies(List<string> cookies)
    {
        foreach (var c in cookies)
        {
            Console.WriteLine(c);
        }
    }

    private static void LogError(string message)
    {
        Console.Error.WriteLine($"[Error]: {message}");
    }
}