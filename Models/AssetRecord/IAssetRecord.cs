using System;
using AssetManagement.Models.AssetHolder;

namespace AssetManagement.Models.AssetRecord
{
    /// <summary>
    /// Interface for a timestamped record for an asset's current state and holder.
    /// </summary>
    public interface IAssetRecord
    {
        string AssetId { get; }
        DateTime Timestamp { get; }
        IAssetHolder Holder { get; }
        AssetState State { get; }
    }
}
