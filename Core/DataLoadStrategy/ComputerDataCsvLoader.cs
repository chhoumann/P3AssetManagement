using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using AssetManagement.Core.Exceptions;
using AssetManagement.DataAccessLibrary.DataModels;

namespace AssetManagement.Core.DataLoadStrategy
{
    public sealed class ComputerDataCsvLoader : CsvLoaderBase<ComputerData>
    {
        private const int FileReadTimeoutInterval = 100;

        protected override uint MaxFileReadAttempts => 50;

        /// <param name="separator">Separator used to separate CSV lines.</param>
        public ComputerDataCsvLoader(char separator) => Separator = separator;

        /// <summary>
        /// Reads data from CSV data file.
        /// </summary>
        /// <returns>An enumerable of the read data.</returns>
        /// <exception cref="FileNotReadException">File could not be read.</exception>
        public override IEnumerable<ComputerData> ReadData(string filePath)
        {
            uint numReadAttempts = 0;

            while (numReadAttempts++ < MaxFileReadAttempts && !IsFileReady(filePath))
            {
                Thread.Sleep(FileReadTimeoutInterval);
            }

            if (numReadAttempts >= MaxFileReadAttempts)
            {
                // TODO: The caller of this method should catch this exception 
                throw new FileNotReadException($"CSV file could not be read at path {filePath}!");
            }
            
            return File
                .ReadAllLines(filePath)
                .Skip(1)
                .Select(csvLine => new ComputerData(filePath, Separator, new CultureInfo("fr-FR"), csvLine));
        }
    }
}