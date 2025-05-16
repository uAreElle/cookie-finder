/*
Quantcast Take-Home Assignment: Most Active Cookie

Command line program that returns the most active cookie for a specific day. 

$ ./[command] -f cookie_log.csv -d 2018-12-09

Current way to run file (fix later):
 dotnet run -- -f cookie_log.csv -d 2018-12-09

Assumptions:
● If multiple cookies meet that criteria, please return all of them on separate lines.
● Please only use additional libraries for testing, logging and cli-parsing. There are libraries for most
languages which make this too easy (e.g pandas) and we’d like you to show off your coding skills.
● You can assume -d parameter takes date in UTC time zone.
● You have enough memory to store the contents of the whole file.
● Cookies in the log file are sorted by timestamp (most recent occurrence is the first line of the file).
*/

using System.Globalization;

public class CookieFinder
{
    private static DateTime SearchDate;

    private static void Main(string[] args)
    {
        (string csvFileName, DateTime searchDate) = GetCommandLineArgs(args);
        SearchDate = searchDate;
        string csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), csvFileName);
        Console.WriteLine(csvFilePath);
        Console.WriteLine(SearchDate);
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
                            Console.WriteLine("Invalid .csv file name.");
                            Environment.Exit(0);
                        }
                        break;

                    case "-d":
                        date = DateTime.ParseExact(args[i + 1], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid arguments.");
            Environment.Exit(0);
        }

        if (string.IsNullOrWhiteSpace(fileName) || date == DateTime.MinValue)
        {
            Console.WriteLine("Unable to parse arguments. Please check -f and -d parameters.");
        }

        return (fileName, date);
    }

}