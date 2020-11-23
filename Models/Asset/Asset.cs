using AssetManagement.Models.AssetHolder;
using AssetManagement.Models.AssetRecord;
using System;
using System.Collections.Generic;

namespace AssetManagement.Models.Asset
{
    public abstract class Asset : IAsset
    {
        /// <summary>
        /// The asset's ID provided by the file from AAF.
        /// </summary>
        public string AssetId { get; private set; }

        /// <summary>
        /// The internal asset database ID. 
        /// </summary>
        public int Id { get; private set; }

        public AssetOwnershipHandler Transfer { get; }

        public DateTime LastChanged => LastAssetRecord.Timestamp;

        public IAssetRecord LastAssetRecord => AssetRecords[AssetRecords.Count - 1];

        public IAssetHolder CurrentAssetHolder => LastAssetRecord.Holder;

        public List<IAssetRecord> AssetRecords { get; } = new List<IAssetRecord>();

        private Asset()
        {
            Transfer = new AssetOwnershipHandler(this);

            // The initial holder of an asset is null because we need an initial AssetRecord for an Asset
            Transfer.ToUser(null);
        }

        public Asset(int id) : this() => Id = id;
        
        public Asset(string assetId, int id) : this()
        {
            AssetId = assetId;
            Id = id;

        }
    }
}