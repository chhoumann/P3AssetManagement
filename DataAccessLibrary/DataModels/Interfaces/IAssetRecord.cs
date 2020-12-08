using AssetManagement.Core;
using System;

namespace AssetManagement.DataAccessLibrary.DataModels.Interfaces
{
    public interface IAssetRecord
    {
        IAsset Asset { get; }
        DateTime Timestamp { get; }
        AssetState State { get; }
        AssetHolder Holder { get; }
    }
}