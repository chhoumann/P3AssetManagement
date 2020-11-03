using AssetManagement.Core;
using System;

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
        public string OriginFileName { get; }

        /// <summary>
        /// Use this constructor if the new asset record isn't invoked by a file
        /// </summary>
        /// <param name="state">Can be online or missing</param>
        /// <param name="holder">The holder of the asset at the time of the record</param>
        public AssetRecord(AssetState state, IAssetHolder holder)
        {
            State = state;
            Holder = holder;
            Date = DateTime.Now;
        }
        /// <summary>
        /// Use this constructor if the new asset record isn't invoked by a file
        /// </summary>
        /// <param name="state">Can be online or missing</param>
        /// <param name="holder">The holder of the asset at the time of the record</param>
        /// <param name="originFileName">The file name of the file where the record comes from</param>
        public AssetRecord(AssetState state, IAssetHolder holder, string originFileName)
        {
            State = state;
            Holder = holder;
            OriginFileName = originFileName;
            Date = DateTime.Now;
        }
    }
}
