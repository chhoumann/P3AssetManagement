using System;
using AssetManagement.Models.AssetHolder;
using AssetManagement.Models.AssetRecord;

namespace AssetManagement.DataAccessLibrary.DataModels
{
    public class AssetRecordData
    {
        // Properties must be both get and set for EntityFrameworkCore to create the object 
        public int Id { get; set; }
        public string FileName { get; set; }
        public string AssetId { get; set; }
        public DateTime Date { get; set; }
        public AssetState State { get; set; }
        public string AssetHolderLabel { get; set; }
        public string AssetHolderUsername { get; set; }
        public string AssetHolderDepartment { get; set; }

        public AssetRecordData(IAssetRecord assetRecord)
        {
            FileName = assetRecord.FileName;
            AssetId = assetRecord.AssetId;
            Date = assetRecord.Date;
            State = assetRecord.State;
            AssetHolderLabel = assetRecord.Holder.Label;
            AssetHolderUsername = assetRecord.Holder.Username;
            AssetHolderDepartment = assetRecord.Holder.Department;
        }

        public AssetRecordData() { } // Empty constructor necessary for EntityFrameworkCore to create the object 

        public IAssetRecord ToIAssetRecord()
        {
            AssetHolder holder = new AssetHolder(AssetHolderLabel, AssetHolderUsername, AssetHolderDepartment);

            return new AssetRecord(State, holder, AssetId, FileName, Date);
        }

        public override string ToString() => $"Filename = {FileName}, State = {State}, Holder = {AssetHolderUsername}, Date = {Date}";
    }
}
