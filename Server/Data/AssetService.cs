﻿using System.Threading.Tasks;
using AssetManagement.DataAccessLibrary;
using AssetManagement.Core;

namespace AssetManagement.Server
{
    public class AssetService
    {
        private readonly SqlDataAccess dataAccess = new SqlDataAccess(new AssetContext());
        
        public async Task<IAsset[]> GetAssetsAsync() => await dataAccess.AssetDataAccess.ReadAllIAsset();

        public async Task DeleteAsset(IAsset asset) => await dataAccess.AssetDataAccess.DeleteIAsset(asset);
    }
}
