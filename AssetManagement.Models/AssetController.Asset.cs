using System;
using System.Collections.Generic;

namespace AssetManagement.Models
{
    public sealed partial class AssetController
    {
        private sealed class Asset : IAssetData
        {
            public string Model { get; }
            public string SerialNumber { get; }

            public int Id { get; }

            public DateTime LastChanged { get; private set; }

            public AssetHolder CurrentAssetHolder => LastAssetRecord.CurrentHolder;
            public AssetRecord LastAssetRecord => AssetRecords[AssetRecords.Count - 1];

            public List<AssetRecord> AssetRecords { get; } = new List<AssetRecord>();
            
            public Asset(int id, string name, string serialNumber)
            {
                Model = name;
                SerialNumber = serialNumber;
                Id = id;

                AssetRecords.Add(new AssetRecord(AssetState.Online, CurrentAssetHolder)); 
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
