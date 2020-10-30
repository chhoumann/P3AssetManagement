using System;
using AssetManagement.Core;

namespace AssetManagement.Models
{
    /// <summary>
    /// A timestamped record for an asset's current state and holder.
    /// </summary>
    public class AssetRecord : IAssetRecord
    {
        public IAssetHolder Holder { get; }
        public DateTime Date { get; }
        public AssetState State { get; }

        public AssetRecord(AssetState state, IAssetHolder holder)
        {
            State = state;
            Holder = holder;
            Date = DateTime.Now;
        }
    }
}
