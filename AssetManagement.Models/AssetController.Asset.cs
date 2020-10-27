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

            public AssetHolder CurrentAssetHolder { get; private set; }
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
                if (newAssetHolder.CurrentAssets.Contains(this))
                {
                    // ERROR: New holder is somehow already holding this asset!
                    throw new ArgumentException("Attempt to add an asset to an asset holder's asset list which already contains this asset!", Model);
                }

                // Remove this asset from its current holder, if any
                if (CurrentAssetHolder != null)
                {
                    if (!CurrentAssetHolder.CurrentAssets.Contains(this))
                    {
                        // ERROR: This asset does not exist in the CurrentAssetHolder's list so we cannot remove it!
                        throw new ArgumentException($"Attempt to remove asset \"{Model}\" from an asset holder's asset list that did not contain the asset!");
                    }

                    CurrentAssetHolder.CurrentAssets.Remove(this);
                }

                // Record this transfer by adding a new transaction to the list of transactions
                AssetRecords.Add(new AssetRecord(LastAssetRecord.State, newAssetHolder));

                // Update the current holder by transferring this asset to the new AssetHolder
                newAssetHolder.CurrentAssets.Add(this);
                CurrentAssetHolder = newAssetHolder;
            }

            public void Dispose() { }
        }
    }
}
