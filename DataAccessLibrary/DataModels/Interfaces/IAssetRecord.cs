using System;
using AssetManagement.Core;

namespace AssetManagement.DataAccessLibrary.DataModels.Interfaces
{
    public interface IAssetRecord
    {
        int Id { get; }
        IAsset Asset { get; }
        DateTime Timestamp { get; }
        AssetState State { get; }
        AssetHolder Holder { get; }
    }
}