using System;
using System.Collections.Generic;
using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels.Handlers;

namespace AssetManagement.DataAccessLibrary.DataModels.Interfaces
{
    public interface IAsset
    {
        string AssetId { get; }
        string SerialNumber { get; }

        IAssetRecord LastAssetRecord { get; }
        DateTime LastChanged { get; }
        AssetHolder CurrentAssetHolder { get; }
        AssetState CurrentState { get; }

        IReadOnlyList<IAssetRecord> AssetRecords { get; }
        AssetOwnershipHandler Transfer { get; }
        AssetStateHandler Is { get; }
    }
}