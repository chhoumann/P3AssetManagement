﻿using AssetManagement.Core;
using System;

namespace AssetManagement.Models
{
    /// <summary>
    /// A timestamped record for an asset's current state and holder.
    /// </summary>
    public class AssetRecord : IAssetRecord
    {
        public string FileName { get; }
        public string AssetId { get; }

        public IAssetHolder Holder { get; }
        public DateTime Date { get; }
        public AssetState State { get; }

        /// <summary>
        /// Create a new AssetRecord where the date defaults to DateTime.now.
        /// </summary>
        /// <param name="state">The PCID state of the asset.</param>
        /// <param name="holder">The holder of the asset at the time of the record.</param>
        public AssetRecord(AssetState state, IAssetHolder holder, string id)
        {
            State = state;
            Holder = holder;
            AssetId = id;

            Date = DateTime.Now;
        }

        /// <summary>
        /// Use this constructor if the new asset record is invoked by a file.
        /// </summary>
        /// <param name="date">The date when the AssetRecord was created</param>
        /// <param name="state">Can be online or missing</param>
        /// <param name="holder">The holder of the asset at the time of the record</param>
        /// <param name="fileName">The file name of the file where the record comes from</param>
        public AssetRecord(DateTime date, AssetState state, IAssetHolder holder, string id, string fileName)
        {
            Date = date;
            State = state;
            Holder = holder;
            AssetId = id;
            FileName = fileName;
        }
    }
}
