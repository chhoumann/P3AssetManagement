using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels.Handlers;
using System;
using System.Collections.Generic;

namespace AssetManagement.DataAccessLibrary.DataModels.Interfaces
{
    public interface IAsset
    {
        /// <summary>
        /// The unique identifyer for an asset - not the database ID, but the ID from the datasource.
        /// </summary>
        string AssetId { get; }
        string SerialNumber { get; }

        IReadOnlyList<IAssetRecord> AssetRecords { get; }
        IAssetRecord LastAssetRecord { get; }
        DateTime LastChanged { get; }
        AssetHolder CurrentHolder { get; }
        AssetState CurrentState { get; }

        AssetOwnershipHandler Transfer { get; }
        AssetStateHandler ChangeState { get; }
    }
}