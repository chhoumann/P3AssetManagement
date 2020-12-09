using System;
using System.Collections.Generic;
using System.Linq;
using AssetManagement.DataAccessLibrary;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;

namespace AssetManagement.Models
{
    public sealed class AssetController<TAsset, TAssetService> 
        where TAsset : IAsset
        where TAssetService : IAssetService<TAsset>, new()
    {
        public AssetController<TAsset, TAssetService> StartWatchingAlienData(AafFileWatcherBase<TAsset> assetFileWatcher)
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