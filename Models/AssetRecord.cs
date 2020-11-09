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
        /// <param name="date">The date when the AssetRecord was created</param>
        /// <param name="state">Can be online or missing</param>
        /// <param name="holder">The holder of the asset at the time of the record</param>
        public AssetRecord(DateTime date, AssetState state, IAssetHolder holder)
        {
            Date = date;
            State = state;
            Holder = holder;
        }

        /// <summary>
        /// Use this constructor if the new asset record isn't invoked by a file
        /// </summary>
        /// <param name="date">The date when the AssetRecord was created</param>
        /// <param name="state">Can be online or missing</param>
        /// <param name="holder">The holder of the asset at the time of the record</param>
        /// <param name="fileName">The file name of the file where the record comes from</param>
        public AssetRecord(DateTime date, AssetState state, IAssetHolder holder, string fileName)
        {
            Date = date;
            State = state;
            Holder = holder;
            FileName = fileName;
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
