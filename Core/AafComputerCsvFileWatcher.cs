using System;
using System.Collections.Generic;
using System.IO;
using AssetManagement.Core.DataLoadStrategy;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.Models.Asset;

namespace AssetManagement.Core
{
    public sealed class AafComputerCsvFileWatcher : AafFileWatcherBase<ComputerData, ComputerAsset>
    {
        public override event Action<List<ComputerAsset>> FileRead;

        public AafComputerCsvFileWatcher(string directoryPath, IAssetLoadStrategy<ComputerData> loadStrategy)
            : base(directoryPath, "*.csv", loadStrategy)
        {
        }

        /// <summary>
        /// Fired when a new CSV file is created.
        /// </summary>
        private protected override void OnNewFile(object eventSource, FileSystemEventArgs e)
        {
            string filePath = e.FullPath;

            List<ComputerAsset> computerAssets = (List<ComputerAsset>) ReadNewDataFile(filePath);

            FileRead?.Invoke(computerAssets);
        }

        /// <summary>
        /// Reads new data file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>IEnumerable of read data.</returns>
        private protected override IEnumerable<ComputerData> ReadNewDataFile(string filePath) => LoadStrategy.ReadData(filePath);

        /// <summary>
        /// Fired when the FileSystemWatcher errors.
        /// </summary>
        // TODO: CATCH THIS PLEASE & Do some error handling / logging
        private protected override void OnError(object sender, ErrorEventArgs e)
        {
            throw e.GetException();
        }
    }
}
