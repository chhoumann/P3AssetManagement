using System;

namespace AssetManagement.DataAccessLibrary
{
    public interface IAssetData
    {
        string Model { get; }
        string SerialNumber { get; }

        int Id { get; }

        DateTime LastChanged { get; }
    }

    public sealed class AssetData : IAssetData
    {
        // Properties must be both get and set for EntityFrameworkCore to create the object 
        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public int Id { get; set; }

        public DateTime LastChanged { get; set; }

        public AssetData(IAssetData asset)
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
