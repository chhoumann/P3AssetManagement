using System;
using System.Collections.Generic;

namespace AssetManagement.Models
{
    public static partial class AssetController
    {
        private sealed class Asset : IAsset
        {
            public string Model { get; }
            public string SerialNumber { get; }

            public int Id { get; }

            public DateTime LastChanged { get; private set; }

            public AssetHolder CurrentAssetHolder { get; private set; }
            public StateRecord CurrentState => StateRecords[StateRecords.Count - 1];

            public List<StateRecord> StateRecords { get; } = new List<StateRecord>();
            public List<Transaction> Transactions { get; } = new List<Transaction>();

            public Asset(int id, string name, string serialNumber)
            {
                Model = name;
                SerialNumber = serialNumber;
                Id = id;

                StateRecords.Add(new StateRecord(AssetState.Online)); 
            }

            public void TransferTo(AssetHolder newAssetHolder)
            {
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

                if (newAssetHolder.CurrentAssets.Contains(this))
                {
                    // ERROR: New holder is somehow already holding this asset!
                    throw new ArgumentException("Attempt to add an asset to an asset holder's asset list which already contains this asset!", Model);
                }

                // Record this transfer by adding a new transaction to the list of transactions
                Transactions.Add(new Transaction(CurrentAssetHolder, newAssetHolder));

                // Update the current holder by transferring this asset to the new AssetHolder
                newAssetHolder.CurrentAssets.Add(this);
                CurrentAssetHolder = newAssetHolder;
            }

            public void Dispose() { }
        }
    }
}
