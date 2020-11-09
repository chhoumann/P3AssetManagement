using AssetManagement.Core;
using System;
using AssetManagement.Models;
using System.Collections.Generic;
using System.Text;

namespace AssetManagement.DataAccessLibrary
{
    public class AssetRecordData
    {
        // Properties must be both get and set for EntityFrameworkCore to create the object 
        public string FileName { get; set; }
        public DateTime Date { get; set; }
        public string AssetId { get; set; }
        public IAssetHolder Holder { get; set; }
        public AssetState State { get; set; }

        public AssetRecordData(IAssetRecord assetRecord)
        {
            FileName = assetRecord.FileName;
            AssetId = assetRecord.AssetId;
            Date = assetRecord.Date;
            Holder = assetRecord.Holder;
            State = assetRecord.State;
        }
        public AssetRecordData() { } // Empty constructor necessary for EntityFrameworkCore to create the object 

        public IAssetRecord ToIAssetRecord()
        {
            return new AssetRecord(FileName, State, Holder, Date);
        }

        public override string ToString()
        {
            return $"Filename = {FileName}, State = {State}, Holder = {Holder}, Date = {Date}";
        }
    }
}
