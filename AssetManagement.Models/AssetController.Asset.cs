using System;
using System.Collections.Generic;

namespace AssetManagement.Models
{
    public sealed partial class AssetController
    {
        private sealed class Asset : IAsset
        {
            public string Model { get; }
            public string SerialNumber { get; }

            public int Id { get; }

            public DateTime LastChanged { get; private set; }

            public AssetRecord LastAssetRecord => AssetRecords[AssetRecords.Count - 1];
            public AssetHolder CurrentAssetHolder => LastAssetRecord.Holder;


            public List<AssetRecord> AssetRecords { get; } = new List<AssetRecord>();

            public Asset(int id, string name, string serialNumber)
            {
                Model = name;
                SerialNumber = serialNumber;
                Id = id;

                // The initial holder of an asset is null because we need an initial AssetRecord for an Asset
                AssetRecords.Add(new AssetRecord(AssetState.Online, null));
            }

            public void TransferTo(AssetHolder newAssetHolder)
            {
                // Record this transfer by adding a new transaction to the list of transactions
                AssetRecords.Add(new AssetRecord(LastAssetRecord.State, newAssetHolder));
            }

            public void Dispose() { }
        }
    }
}
