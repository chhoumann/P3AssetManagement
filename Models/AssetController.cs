using System;
using System.Collections.Generic;
using System.IO;
using AssetManagement.Core.DataLoadStrategy;
using AssetManagement.DataAccessLibrary.DataModels;

namespace AssetManagement.Models
{
    // TODO: Make this generic
    public sealed class AssetController
    {
        private readonly List<Computer> assets = new List<Computer>();
        private readonly string filePath = Directory.GetParent(Environment.CurrentDirectory) + @"/AAFData";

        public AssetController StartWatchingAlienData()
        {
            new AafComputerCsvFileWatcher(filePath, new ComputerDataCsvLoader(';'))
                .StartWatching()
                .FileRead += OnNewData;

            return this;
        }

        private void OnNewData(IEnumerable<Computer> assetsInList)
        {
            AssetComparer<Computer> assetComparer = new AssetComparer<Computer>(assets);

            assetComparer.NewAssetsFound += OnNewAssetsFound;
            assetComparer.OnNewData(assetsInList);
        }

        private void OnNewAssetsFound(List<Computer> addedAssets)
        {
            // TODO: Connect to DB and add the new assets
            assets.AddRange(addedAssets);
        }
    }
}