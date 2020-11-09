using AssetManagement.Core;
using System;

namespace AssetManagement.DataAccessLibrary
{
    // Should implement an interface later
    public sealed class AssetData
    {
        // Properties must be both get and set for EntityFrameworkCore to create the object 
        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public int Id { get; set; }

        public DateTime LastChanged { get; set; }

        public AssetData(IAsset asset)
        {
            Model = asset.Model;
            SerialNumber = asset.SerialNumber;
            Id = asset.DbId;
            LastChanged = asset.LastChanged;
        }
        public AssetData() { } // Empty constructor necessary for EntityFrameworkCore to create the object 

        public IAsset ToIAsset()
        {
            return Models.AssetController.MakeAsset(Id, Model, SerialNumber);
        }

        public override string ToString()
        {
            return $"Id = {Id}, SerialNumber = {SerialNumber}, Model = {Model}, LastChanged = {LastChanged}";
        }
    }
}
