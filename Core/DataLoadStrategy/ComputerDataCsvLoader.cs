using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using AssetManagement.Core.Exceptions;
using AssetManagement.DataAccessLibrary.DataModels;

namespace AssetManagement.Core.DataLoadStrategy
{
    public sealed class CsvLoader : CsvLoaderBase<ComputerData>
    {
        private const int FileReadTimeoutInterval = 100;

        protected override uint MaxFileReadAttempts => 50;

        /// <param name="separator">Separator used to separate CSV lines.</param>
        /// <param name="filePath">File path for CSV file.</param>
        public CsvLoader(char separator, string filePath)
        {
            FilePath = filePath;
            Separator = separator;
        }

        /// <summary>
        /// Reads data from CSV data file.
        /// </summary>
        /// <returns>An enumerable of the read data.</returns>
        /// <exception cref="FileNotReadException">File could not be read.</exception>
        public override IEnumerable<ComputerData> ReadData()
        {
            uint numReadAttempts = 0;

            while (numReadAttempts++ < MaxFileReadAttempts && !IsFileReady())
            {
                Thread.Sleep(FileReadTimeoutInterval);
            }

            if (numReadAttempts >= MaxFileReadAttempts)
            {
                // TODO: The caller of this method should catch this exception 
                throw new FileNotReadException($"CSV file could not be read at path {FilePath}!");
            }
            
            return File
                .ReadAllLines(FilePath)
                .Skip(1)
                .Select(csvLine => new ComputerData(FilePath, Separator, new CultureInfo("fr-FR"), csvLine));
        }
    }
}