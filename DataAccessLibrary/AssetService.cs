using AssetManagement.DataAccessLibrary.Contexts;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Handlers;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using System;
using System.Linq;

namespace AssetManagement.DataAccessLibrary
{
    public abstract class AssetService<TAsset, TAssetRecord, TDbContext>
        where TAsset : IAsset
        where TAssetRecord : IAssetRecord
        where TDbContext : AssetContext
    {
        protected AssetService()
        {
            AssetOwnershipHandler.AssetOwnerShipChanged += OnAssetUpdated;
            AssetStateHandler.AssetStateChanged += OnAssetUpdated;
            InsertDepotAndCageToDb();
        }

        public abstract event Action AssetUpdated;

        protected abstract void OnAssetUpdated();

        protected abstract TDbContext Db { get; }

        public string DepotUsername { get; } = "depot";
        public string CageUsername { get; } = "cage";

        public AssetHolder Depot => Db.AssetHolders.Single(holder => holder.Username == DepotUsername);
        public AssetHolder Cage => Db.AssetHolders.Single(holder => holder.Username == CageUsername);

        protected abstract TAssetRecord InitialRecord(TAsset asset);

        public abstract TAsset[] GetAssets();
        public abstract TAsset GetAssetById(string id);
        public abstract void AddAsset(IAsset asset);
        public abstract void DeleteAsset(IAsset asset);

        private void InsertDepotAndCageToDb()
        {
            if (Db.AssetHolders.SingleOrDefault(h => h.Username == DepotUsername) == null)
            {
                Db.AssetHolders.Add(new AssetHolder("Depot", DepotUsername));
            }

            if (Db.AssetHolders.SingleOrDefault(h => h.Username == CageUsername) == null)
            {
                Db.AssetHolders.Add(new AssetHolder("Cage", CageUsername));
            }

            Db.SaveChanges();
        }

        protected AssetHolder GetAssetHolderByUsername(string username) =>
            Db.AssetHolders.Single(x => x.Username == username);
    }
}