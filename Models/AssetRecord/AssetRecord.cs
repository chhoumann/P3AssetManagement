using AssetManagement.Models.AssetHolder;
using System;

namespace AssetManagement.Models.AssetRecord
{
    /// <summary>
    /// A timestamped record for an asset's current state and holder.
    /// </summary>
    public class AssetRecord : IAssetRecord
    {
        public string AssetId { get; }

        public IAssetHolder Holder { get; }
        public DateTime Timestamp { get; }
        public AssetState State { get; }

        /// <summary>
        /// Create a new AssetRecord where the date defaults to DateTime.now.
        /// Use this constructor if the new asset record isn't invoked by a file,
        /// And the Timestamp of the record should be set to the current time
        /// </summary>
        /// <param name="state">The PCID state of the asset.</param>
        /// <param name="holder">The holder of the asset at the time of the record.</param>
        /// <param name="assetId">The PCID asset ID.</param>
        public AssetRecord(AssetState state, IAssetHolder holder, string assetId)
        {
            State = state;
            Holder = holder;
            AssetId = assetId;

            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// Use this constructor if the new asset record is indeed invoked by an external recource.
        /// </summary>
        /// <param name="state">Can be online or missing</param>
        /// <param name="holder">The holder of the asset at the time of the record</param>
        /// <param name="assetId">The PCID asset ID.</param>
        /// <param name="date">The date when the AssetRecord was created</param>
        public AssetRecord(AssetState state, IAssetHolder holder, string assetId, DateTime date)
        {
            Timestamp = date;
            State = state;
            Holder = holder;
            AssetId = assetId;
        }
    }
}
