using AssetManagement.DataAccessLibrary.Generic;
using AssetManagement.Models.Asset;
using AssetController = AssetManagement.Models;

namespace AssetManagement.DataAccessLibrary
{
    // Should implement an interface later
    public sealed class AssetData : IAsset 
    {
        // Properties must be both get and set for EntityFrameworkCore to create the object 
        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public int Id { get; set; }

        public string AssetId => throw new System.NotImplementedException();

        public int DbId => Id;

        public AssetData(IAsset asset)
        {
            Model = asset.Model;
            SerialNumber = asset.SerialNumber;
            Id = asset.DbId;
        }
 
        public AssetData() { } // Empty constructor necessary for EntityFrameworkCore to create the object 

        public override string ToString()
        {
            return $"Id = {Id}, SerialNumber = {SerialNumber}, Model = {Model}";
        }
    }
}
