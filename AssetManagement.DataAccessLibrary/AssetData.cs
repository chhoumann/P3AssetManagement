using System;
using AssetManagement.Models;

namespace AssetManagement.DataAccessLibrary
{
    public sealed class AssetData
    {
        // Properties must be both get and set for EntityFrameworkCore to create the object 
        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public int Id { get; set; }

        public DateTime LastChanged { get; set; }

        //public List<StateRecord> StateRecords { get; }

        //public List<Transaction> Transactions { get; }

        public AssetData(IAsset asset)
        {
            Model = asset.Model;
            SerialNumber = asset.SerialNumber;
            Id = asset.Id;
            LastChanged = asset.LastChanged;
        }

        public AssetData() { } // Empty constructor necessary for EntityFrameworkCore to create the object 

        public override string ToString()
        {
            return $"Id = {Id}, SerialNumber = {SerialNumber}, Model = {Model}, LastChanged = {LastChanged}";
        }
    }
}
