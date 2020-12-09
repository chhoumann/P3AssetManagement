using System;
using System.Collections.Generic;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;

namespace AssetManagement.DataAccessLibrary
{
    public interface IAssetService<TAsset> where TAsset : IAsset
    {
        event Action AssetUpdated;
        AssetHolder Cage { get; }
        AssetHolder Depot { get; }
        TAsset[] GetAssets();
        TAsset GetAssetById(string id);
        void AddAsset(TAsset asset);
        void AddAssets(IEnumerable<TAsset> assets);
        void DeleteAsset(TAsset asset);
    }
}