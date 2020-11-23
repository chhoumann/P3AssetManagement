using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AssetManagement.Core.DataLoadStrategy;
using AssetManagement.DataAccessLibrary.DbContexts;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.Generic;

namespace AssetManagement.Core
{
    public sealed class AafCsvDataWatcher
    {
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

            watcher.Created += OnChanged;
            watcher.Error += OnError;

            watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Fired when a new CSV file is created.
        /// </summary>
        private async void OnChanged(object eventSource, FileSystemEventArgs e)
        {
            string filePath = e.FullPath;
            AssetLoadCsvContext<ComputerData> assetLoadCsvContext = new AssetLoadCsvContext<ComputerData>(new ComputerDataCsvStrategy(';'));
            List<ComputerData> computerData = assetLoadCsvContext.LoadData(filePath);

            await SaveComputerDataToDb(computerData);
        }

        /// <summary>
        /// Fired when the FileSystemWatcher errors.
        /// </summary>
        private void OnError(object sender, ErrorEventArgs e)
        {
            throw e.GetException();
        }
        
        /// <summary>
        /// Save a list of ComputerData to the database.
        /// </summary>
        /// <param name="computerData">The data to save.</param>
        /// <returns>An awaitable task.</returns>
        private async Task SaveComputerDataToDb(List<ComputerData> computerData)
        {
            Console.WriteLine("New alien data recieved, saving to DB...");
            
            SqlDataAccess<ComputerData> computerDataAccess = new SqlDataAccess<ComputerData>(new ComputerDataContext());

            await computerDataAccess.InsertRange(computerData);
            await computerDataAccess.Save();
            
            Console.WriteLine("Saved new file to DB.");
        }
    }
}