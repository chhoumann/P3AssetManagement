using System;
using System.Collections.Generic;
using System.IO;
using AssetManagement.Core.DataLoadStrategy;
using AssetManagement.Models.Asset;

namespace AssetManagement.Core
{
    public sealed class AssetController
    {
        private readonly string filePath = Directory.GetParent(Environment.CurrentDirectory) + @"/AAFData";
        private readonly List<Asset> assets = new List<Asset>();

        public AssetController StartWatchingAlienData()
        {
            new AafComputerCsvFileWatcher(filePath, new ComputerDataCsvLoader(';'))
                .StartWatching()
                .FileRead += OnNewData;
            
            return this;
        }

        private void OnNewData(List<ComputerAsset> assetsInList)
        {
            AssetComparer<Asset> assetComparer = new AssetComparer<Asset>(assets);
            
            assetComparer.NewAssetsFound += OnNewAssetsFound;
            assetComparer.OnNewData(assetsInList);
        }

        private void OnNewAssetsFound(List<Asset> addedAssets)
        {
            // TODO: Connect to DB and add the new assets
            assets.AddRange(addedAssets);
        }
    }
}
