using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssetManagement.Core.DataLoadStrategy;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.Models.DataLoadStrategy;
using AssetManagement.Server;

namespace AssetManagement.Models
{
    // TODO: Make this generic
    public sealed class AssetController
    {
        private readonly string filePath = Directory.GetParent(Environment.CurrentDirectory) + @"/AAFData";

        public AssetController StartWatchingAlienData()
        {
            new AafComputerCsvFileWatcher(filePath, new ComputerCsvLoader(';'))
                .StartWatching()
                .FileRead += OnNewData;

            return this;
        }

        private void OnNewData(IEnumerable<Computer> assetsInList)
        {
            ComputerService computerService = new ComputerService();
            List<Computer> currentAssets = computerService.GetAssets().ToList();

            Console.WriteLine(currentAssets.Count);
            
            AssetComparer<Computer> assetComparer = new AssetComparer<Computer>(currentAssets);

            assetComparer.NewAssetsFound += OnNewAssetsFound;
            assetComparer.OnNewData(assetsInList);
        }

        private void OnNewAssetsFound(List<Computer> addedAssets)
        {
            // TODO: Connect to DB and add the new assets
            ComputerService computerService = new ComputerService();
            computerService.AddAssets(addedAssets);
        }
    }
}