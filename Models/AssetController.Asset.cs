using AssetManagement.Core;
using System;
using System.Collections.Generic;

namespace AssetManagement.Models
{
    public sealed partial class AssetController
    {
        /// <summary>
        /// The private, core implementation of IAsset which is only visible to AssetController.
        /// </summary>
        private sealed class Asset : IAsset
        {
            public string Model { get; }
            public string SerialNumber { get; }

            /// <summary>
            /// The asset's ID provided by the file from AAF.
            /// </summary>
            public string AssetId { get; }
            
            /// <summary>
            /// The internal asset database ID. 
            /// </summary>
            public int DbId { get; }

            public DateTime LastChanged { get; private set; }

            public IAssetRecord LastAssetRecord => AssetRecords[AssetRecords.Count - 1];
            public IAssetHolder CurrentAssetHolder => LastAssetRecord.Holder;

            public List<IAssetRecord> AssetRecords { get; } = new List<IAssetRecord>();

            public Asset(int id, string model, string serialNumber)
            {
                Model = model;
                SerialNumber = serialNumber;
                DbId = id;

                // The initial holder of an asset is null because we need an initial AssetRecord for an Asset
                AssetRecords.Add(new AssetRecord(AssetState.Null, null, AssetId));
            }

            /// <summary>
            /// Transfer the asset to a new asset holder.
            /// </summary>
            /// <param name="newAssetHolder">The asset holder to transfer to.</param>
            public void TransferTo(IAssetHolder newAssetHolder)
            {
                // Record this transfer by adding a new transaction to the list of transactions
                AssetRecords.Add(new AssetRecord(LastAssetRecord.State, newAssetHolder, AssetId));
            }

            public void Dispose() { }
        }
    }
}
