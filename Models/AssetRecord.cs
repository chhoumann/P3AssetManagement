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
        public IAssetHolder Holder { get; }
        public DateTime Date { get; }
        public AssetState State { get; }

        /// <summary>
        /// Create a new AssetRecord where the date defaults to DateTime.now.
        /// Use this constructor if the new asset record isn't invoked by a file,
        /// And the Date of the record should be set to the current time
        /// </summary>
        /// <param name="state">The PCID state of the asset.</param>
        /// <param name="holder">The holder of the asset at the time of the record.</param>
        public AssetRecord(AssetState state, IAssetHolder holder)
        {
            State = state;
            Holder = holder;
            Date = DateTime.Now;
        }

        /// <summary>
        /// Create a new AssetRecord with a specific date.
        /// Use this constructor if the new asset record isn't invoked by a file,
        /// and you want to specify the time yourself.
        /// </summary>
        /// <param name="state">The PCID state of the asset.</param>
        /// <param name="holder">The holder of the asset at the time of the record.</param>
        /// <param name="date">The date when the AssetRecord was created.</param>
        public AssetRecord(AssetState state, IAssetHolder holder, DateTime date)
        {
            Date = date;
            State = state;
            Holder = holder;
        }

        /// <summary>
        /// Use this constructor if the new asset record is invoked by a file.
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
    }
}
