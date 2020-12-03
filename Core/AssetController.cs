using System;
using System.Collections.Generic;
using System.IO;
using AssetManagement.Models.Asset;

namespace AssetManagement.Core
{
    /// <summary>
    /// Factory controller responsible for creating assets, transferring assets and updating asset states.
    /// </summary>
    public sealed class AssetController
    {
        // Sole responsibility of this class should be to periodically update our assets.
        // Updating asset ownership is done by the asset itself (which calls an event to add record)
        private readonly string filePath = Directory.GetParent(Environment.CurrentDirectory) + @"/AAFData";
        private readonly List<Asset> assets = new List<Asset>();

        public AssetController StartWatchingAlienData()
        {
            AafCsvDataWatcher aafCsvDataWatcher = new AafCsvDataWatcher(filePath);
            aafCsvDataWatcher.StartWatching();
            aafCsvDataWatcher.FileRead += OnNewData;
            
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
