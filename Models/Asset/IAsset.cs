using System;
using System.Collections.Generic;
using AssetManagement.Models.AssetHolder;
using AssetManagement.Models.AssetRecord;

namespace AssetManagement.Models.Asset
{
    public interface IAsset
    {
        string Model { get; }
        string SerialNumber { get; }
        string AssetId { get; }
        int DbId { get; }

        DateTime LastChanged { get; }

        IAssetHolder CurrentAssetHolder { get; }
        IAssetRecord LastAssetRecord { get; }
        List<IAssetRecord> AssetRecords { get; }

        void TransferTo(IAssetHolder receiver);
    }
}