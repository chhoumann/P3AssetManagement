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

        public IAssetRecord LastAssetRecord => AssetRecords[AssetRecords.Count - 1];

        public DateTime LastChanged => LastAssetRecord.Timestamp;

        public IAssetHolder CurrentAssetHolder => LastAssetRecord.Holder;

        public List<IAssetRecord> AssetRecords { get; } = new List<IAssetRecord>();

        private Asset()
        {
            Transfer = new AssetOwnershipHandler(this);

            // The initial holder of an asset is the depot,
            // to make properties: LastAssetRecord, LastChanged and CurrentAssetHolder needs a record
            Transfer.ToDepot();
        }

        protected Asset(int id) : this() => Id = id;

        protected Asset(string assetId, int id) : this()
        {
            AssetId = assetId;
            Id = id;
        }
    }
}
