using System.Globalization;
using AssetManagement.DataReader;

namespace AssetManagement.Models.LoadDataFromFiles
{
    /// <summary>
    /// This interface defines how a line-parser class should look.
    /// The job of a line-parser class is to parse a line of csv-data into the wanted format.
    /// </summary>
    interface ICsvLineParser
    {
        CultureInfo CultureInfo { get; }

        /// <summary>
        /// Should parse the Line property to an IAssetRecord according to the provided parserFunc
        /// </summary>
        /// <returns>Returns an IAssetRecord, which is an interface, that is useful throughout the rest of the program</returns>
        CsvLineParser GetParseFunc();
    }
}
