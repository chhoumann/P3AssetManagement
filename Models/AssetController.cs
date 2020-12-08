using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssetManagement.Core.DataLoadStrategy;
using AssetManagement.DataAccessLibrary;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using AssetManagement.Models.DataLoadStrategy;

namespace AssetManagement.Models
{
    public sealed class AssetController<TAsset, TAssetService> 
        where TAsset : IAsset
        where TAssetService : IAssetService<TAsset>, new()
    {
        private readonly string filePath = Directory.GetParent(Environment.CurrentDirectory) + @"/AAFData";

        public AssetController<TAsset, TAssetService> StartWatchingAlienData(AafFileWatcherBase<TAsset, TAsset> assetFileWatcher)
        {
            assetFileWatcher.StartWatching().FileRead += OnNewData;

            return this;
        }

        private void OnNewData(IEnumerable<TAsset> assetsInList)
        {
            TAssetService assetService = new TAssetService();
            List<TAsset> currentAssets = assetService.GetAssets().ToList();

            Console.WriteLine(currentAssets.Count);
            
            AssetComparer<TAsset> assetComparer = new AssetComparer<TAsset>(currentAssets);

            assetComparer.NewAssetsFound += OnNewAssetsFound;
            assetComparer.OnNewData(assetsInList);
        }

        private void OnNewAssetsFound(List<TAsset> addedAssets)
        {
            TAssetService assetService = new TAssetService();
            assetService.AddAssets(addedAssets);
        }
    }
}