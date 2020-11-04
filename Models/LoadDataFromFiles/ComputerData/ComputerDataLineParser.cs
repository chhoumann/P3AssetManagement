using AssetManagement.Core;
using AssetManagement.DataReader;
using System;
using System.Globalization;

namespace AssetManagement.Models
{
    public class ComputerDataLineParser : ICsvLineParser
    {
        public CultureInfo CultureInfo { get; } = new CultureInfo("fr-FR");

        public CsvLineParser GetParseFunc()
        {
            return (string originFileName, string[] fields) =>
            {
                // Parses the string-array to a ComputerData object
                // Be aware that the DateTime.Parse() could throw FormatException() if the data given is not valid
                ComputerData parsedData = new ComputerData(
                    originFileName,
                    DateTime.Parse(fields[0], CultureInfo),
                    fields[1],
                    fields[2],
                    fields[3],
                    fields[4],
                    fields[5],
                    fields[6],
                    fields[7],
                    fields[8],
                    fields[9],
                    fields[10]
                );

                return parsedData;
            };
        }
    }
}
