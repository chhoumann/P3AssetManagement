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
        string FileName { get; set; }
        DateTime Date { get; set; }
        IAssetHolder Holder { get; set; }
        AssetState State { get; set; }

        public AssetRecordData(IAssetRecord assetRecord)
        {
            FileName = assetRecord.FileName;
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
