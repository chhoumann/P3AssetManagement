using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.Models.DataLoadStrategy;

namespace AssetManagement.Models
{
    public sealed class AafComputerCsvFileWatcher : AafFileWatcherBase<Computer>
    {
        public AafComputerCsvFileWatcher(string directoryPath, IAssetLoadStrategy<Computer> loadStrategy)
            : base(directoryPath, "*.csv", loadStrategy)
        {
        }

        public override event Action<IEnumerable<Computer>> FileRead;

        /// <summary>
        ///     Fired when a new CSV file is created.
        /// </summary>
        private protected override void OnNewFile(object eventSource, FileSystemEventArgs e)
        {
            string filePath = e.FullPath;

            IEnumerable<Computer> computerAssets = ReadNewDataFile(filePath).ToList();

            FileRead?.Invoke(computerAssets);
        }

        /// <summary>
        ///     Reads new data file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>IEnumerable of read data.</returns>
        private protected override IEnumerable<Computer> ReadNewDataFile(string filePath)
        {
            return LoadStrategy.ReadData(filePath);
        }

        /// <summary>
        ///     Fired when the FileSystemWatcher errors.
        /// </summary>
        private protected override void OnError(object sender, ErrorEventArgs e)
        {
            throw e.GetException();
        }
    }
}