using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Core.DataLoadStrategy;
using AssetManagement.Core.Exceptions;
using AssetManagement.DataAccessLibrary.DbContexts;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.Generic;
using AssetManagement.Models.Asset;
using AssetManagement.Models.AssetHolder;

namespace AssetManagement.Core
{
    public sealed class AafCsvDataWatcher
    {
        public event Action<List<ComputerAsset>> FileRead;

        private readonly string path;

        public AafCsvDataWatcher(string path) => this.path = path;


        /// <summary>
        /// Start watching for new CSV files at the path the class was initialized with.
        /// </summary>
        public void StartWatching()
        {
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = path,
                NotifyFilter = NotifyFilters.FileName,
                Filter = "*.csv"
            };

            watcher.Created += OnNewFile;
            watcher.Error += OnError;

            watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Fired when a new CSV file is created.
        /// </summary>
        private void OnNewFile(object eventSource, FileSystemEventArgs e)
        {
            string filePath = e.FullPath;

            List<ComputerAsset> computerAssets = ReadNewDataFile(filePath)
                .Select(computerData => (ComputerAsset) computerData)
                .ToList();

            FileRead?.Invoke(computerAssets);
        }

        /// <summary>
        /// Reads new data file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>IEnumerable of read data.</returns>
        private IEnumerable<ComputerData> ReadNewDataFile(string filePath)
        {
            return new CsvLoader(';', filePath).ReadData();
        }

        /// <summary>
        /// Fired when the FileSystemWatcher errors.
        /// </summary>
        // TODO: CATCH THIS PLEASE & Do some error handling / logging
        private void OnError(object sender, ErrorEventArgs e)
        {
            throw e.GetException();
        }

        /// <summary>
        /// Save a list of ComputerData to the database.
        /// </summary>
        /// <param name="computerData">The data to save.</param>
        /// <returns>An awaitable task.</returns>
        private async Task SaveComputerDataToDb(IEnumerable<ComputerData> computerData)
        {
            Console.WriteLine("New alien data received, saving to DB...");

            SqlDataAccess<ComputerData> computerDataAccess = new SqlDataAccess<ComputerData>(new ComputerDataContext());

            await computerDataAccess.InsertRange(computerData);
            await computerDataAccess.Save();

            Console.WriteLine("Saved new file to DB.");
        }
    }
}
