using System.Globalization;
using System.IO;
using AssetManagement.DataAccessLibrary.DataModels;

namespace AssetManagement.Core.DataLoadStrategy
{
    public sealed class ComputerDataCsvStrategy : IAssetLoadStrategy<ComputerData>
    {
        private readonly char separator;
        
        /// <param name="separator">The separator between each csv column</param>
        public ComputerDataCsvStrategy(char separator) => this.separator = separator;

        /// <summary>
        /// Reads data from csv line, and returns a ComputerData object.
        /// </summary>
        /// <returns>A list of ComputerData objects.</returns>
        public ComputerData ReadAssetFromCsvLine(string csvLine, string filePath)
        {
            return new ComputerData(Path.GetFileName(filePath), separator, new CultureInfo("fr-FR"), csvLine);
        }
    }
}