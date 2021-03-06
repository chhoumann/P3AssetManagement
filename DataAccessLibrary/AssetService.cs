using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.DataAccessLibrary.Contexts;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Handlers;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;

namespace AssetManagement.DataAccessLibrary
{
    /// <summary>
    /// A base class for all service classes
    /// </summary>
    /// <typeparam name="TAsset">The datamodel type</typeparam>
    /// <typeparam name="TAssetRecord">The datamodels asset record type</typeparam>
    /// <typeparam name="TDbContext">The database context type</typeparam>
    public abstract class AssetService<TAsset, TAssetRecord, TDbContext> : IStaticAssetHolders, IAssetService<TAsset> where TAsset : IAsset
        where TAssetRecord : IAssetRecord
        where TDbContext : AssetContext
    {
        protected AssetService()
        {
            InsertDepotAndCageToDb();
            
            Cage = Db?.AssetHolders.Single(holder => holder.Username == CageUsername);
            Depot = Db?.AssetHolders.Single(holder => holder.Username == DepotUsername);
        }

        private string DepotUsername { get; } = "depot";
        private string CageUsername { get; } = "cage";

        public AssetHolder Depot { get; }
        public AssetHolder Cage { get; }
        

        /// <summary>
        /// Makes sure that a cage and a depot is always present in the database.
        /// </summary>
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

        #region Abstract properties and methods
        protected abstract TDbContext Db { get; }

        /// <summary>
        /// Signals that an asset has been updated in the database.
        /// </summary>
        public abstract event Action AssetUpdated;

        /// <summary>
        /// This method is invoked by a property change in an asset,
        /// which saves the changes to the database and invokes the AssetUpdated event.
        /// </summary>
        protected abstract void OnAssetUpdated();

        /// <summary>
        /// Gets the initial record for an asset.
        /// </summary>
        /// <param name="asset">The asset which the record refers to</param>
        /// <returns>The initial record present in an asset</returns>
        protected abstract TAssetRecord InitialRecord(TAsset asset);

        /// <summary>
        /// Gets all assets from the database.
        /// </summary>
        /// <returns>An array of all assets from the database.</returns>
        public abstract TAsset[] GetAssets();
        
        /// <summary>
        /// Gets an asset from the database by the asset's ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The asset with the given ID.</returns>
        public abstract TAsset GetAssetById(string id);

        public abstract TAsset GetAssetBySerialNumber(string serialNumber);

        /// <summary>
        /// Adds a new asset to the database.
        /// </summary>
        /// <param name="asset">The asset to be added</param>
        public abstract void AddAsset(TAsset asset);
        public abstract void AddAssets(IEnumerable<TAsset> assets);

        /// <summary>
        /// Deletes an asset from the database.
        /// </summary>
        /// <param name="asset">The asset to be deleted.</param>
        public abstract void DeleteAsset(TAsset asset);
        #endregion
    }
}