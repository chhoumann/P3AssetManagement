using System;
using System.Collections.Generic;
using AssetManagement.Models.AssetHolder;
using AssetManagement.Models.AssetRecord;

namespace AssetManagement.Models.Asset
{
    public class Asset : IAsset
    {
        public string Model { get; private set; }
        public string SerialNumber { get; private set; }

        /// <summary>
        /// The asset's ID provided by the file from AAF.
        /// </summary>
        public string AssetId { get; private set; }

        /// <summary>
        /// The internal asset database ID. 
        /// </summary>
        public int Id { get; private set; }

        public AssetOwnershipManager Transfer { get; }

        public DateTime LastChanged => LastAssetRecord.Date;

        public IAssetRecord LastAssetRecord => AssetRecords[AssetRecords.Count - 1];

        public IAssetHolder CurrentAssetHolder => LastAssetRecord.Holder;

        public List<IAssetRecord> AssetRecords { get; } = new List<IAssetRecord>();

        public Asset()
        {
            Transfer = new AssetOwnershipManager(this);

            // TODO: EVENT
            // The initial holder of an asset is null because we need an initial AssetRecord for an Asset
            AssetRecords.Add(new AssetRecord.AssetRecord(AssetState.Null, null, AssetId));
        }

        public Asset(int id, string model, string serialNumber) : this()
        {
            Model = model;
            SerialNumber = serialNumber;
            Id = id;
            AssetId = id.ToString(); // TODO: Fix -- do we use AssetId or Id? Or Both.
        }
    }
}