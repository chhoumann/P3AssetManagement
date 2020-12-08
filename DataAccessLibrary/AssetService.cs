using System;
using System.Collections.Generic;
using System.Linq;
using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.Contexts;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Handlers;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;

namespace AssetManagement.Server
{
    public abstract class AssetService<TAsset, TAssetRecord, TDbContext>
        where TAsset : IAsset
        where TAssetRecord : IAssetRecord
        where TDbContext : AssetContext
    {
        public AssetService()
        {
            AssetOwnershipHandler.AssetHolderChanged += CreateNewAssetRecord;
            AssetStateHandler.AssetStateChanged += CreateNewAssetRecord;
            InsertDepotAndCageToDb();
        }

        protected abstract TDbContext Db { get; }

        protected AssetHolder Depot => Db.AssetHolders.Single(holder => holder.Username == StaticUsernames.Depot);
        protected AssetHolder Cage => Db.AssetHolders.Single(holder => holder.Username == StaticUsernames.Cage);
        public abstract event Action AssetUpdated;

        protected abstract TAssetRecord InitialRecord(TAsset asset);

        public abstract TAsset[] GetAssets();
        public abstract TAsset GetAssetById(string id);
        public abstract void AddAsset(IAsset asset);
        public abstract void AddAssets(IEnumerable<IAsset> assets);
        public abstract void DeleteAsset(IAsset asset);

        protected abstract void CreateNewAssetRecord(IAsset asset, string assetHolderUsername, DateTime timestamp,
            AssetState state);

        protected abstract void CreateNewAssetRecord(IAsset asset, AssetHolder holder, DateTime timestamp,
            AssetState state);

        private void InsertDepotAndCageToDb()
        {
            if (Db.AssetHolders.SingleOrDefault(h => h.Username == StaticUsernames.Depot) == null)
            {
                Db.AssetHolders.Add(new AssetHolder("Depot", StaticUsernames.Depot));
            }

            if (Db.AssetHolders.SingleOrDefault(h => h.Username == StaticUsernames.Cage) == null)
            {
                Db.AssetHolders.Add(new AssetHolder("Cage", StaticUsernames.Cage));
            }

            Db.SaveChanges();
        }


        protected AssetHolder GetAssetHolderByUsername(string username) =>
            Db.AssetHolders.Single(x => x.Username == username);
    }
}