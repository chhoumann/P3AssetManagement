using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssetManagement.DataAccessLibrary;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.Models.DataLoadStrategy;

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

        private void OnNewData(List<Computer> assetsInList)
        {
            List<Computer> currentAssets = ComputerService.Instance.GetAssets().ToList();

            AssetComparer<Computer> assetComparer = new AssetComparer<Computer>(currentAssets);

            assetComparer.NewAssetsFound += OnNewAssetsFound;
            assetComparer.OnNewData(assetsInList);
        }

        private void OnNewAssetsFound(List<Computer> addedAssets)
        {
            ComputerService.Instance.AddAssets(addedAssets);
        }
    }
}