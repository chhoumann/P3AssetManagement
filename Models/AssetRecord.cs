using AssetManagement.Core;
using System;

namespace AssetManagement.Models
{
    /// <summary>
    /// A timestamped record for an asset's current state and holder.
    /// </summary>
    public class AssetRecord : IAssetRecord
    {
        public string FileName { get; }
        public IAssetHolder Holder { get; }
        public DateTime Date { get; }
        public AssetState State { get; }

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
        /// <param name="fileName">The file name of the file where the record comes from</param>
        public AssetRecord(string fileName, AssetState state, IAssetHolder holder)
        {
            FileName = fileName;
            State = state;
            Holder = holder;
            Date = DateTime.Now;
        }

        /// <summary>
        /// Use this constructor if the new asset record is indeed invoked by a file
        /// </summary>
        /// <param name="fileName">Can be online or missing</param>
        /// <param name="state">The holder of the asset at the time of the record</param>
        /// <param name="holder">The file name of the file where the record comes from</param>
        /// <param name="date">The date at which the record was made</param>
        public AssetRecord(string fileName, AssetState state, IAssetHolder holder, DateTime date)
        {
            FileName = fileName;
            State = state;
            Holder = holder;
            Date = date;
        }
    }
}
