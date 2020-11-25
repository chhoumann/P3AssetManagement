using System;
using System.Collections.Generic;
using AssetManagement.Models.AssetHolder;
using AssetManagement.Models.AssetRecord;

namespace AssetManagement.Models.Asset
{
    public interface IAsset
    {
        string AssetId { get; }
        string Id { get; }

        DateTime LastChanged { get; }

        IAssetHolder CurrentAssetHolder { get; }
        IAssetRecord LastAssetRecord { get; }
        List<IAssetRecord> AssetRecords { get; }

        AssetOwnershipHandler Transfer { get; }
    }
}