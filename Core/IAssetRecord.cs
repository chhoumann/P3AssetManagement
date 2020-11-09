using System;

namespace AssetManagement.Core
{
    /// <summary>
    /// Interface for a timestamped record for an asset's current state and holder.
    /// </summary>
    public interface IAssetRecord
    {
        string FileName { get; }
        string AssetId { get; }
        DateTime Date { get; }
        IAssetHolder Holder { get; }
        AssetState State { get; }
    }
}
