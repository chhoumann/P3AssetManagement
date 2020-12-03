using AssetManagement.Models.AssetHolder;

namespace AssetManagement.Models.Asset
{
    public class ComputerAsset : Asset
    {
        public string Model { get; }
        public string SerialNumber { get; }
        
        public ComputerAsset(string model, string serialNumber, string assetId)
        {
            Model = model;
            SerialNumber = serialNumber;
            AssetId = assetId;
        }
        
        public ComputerAsset(IAssetHolder assetHolder, string assetId, string model, string serialNumber) : base(assetHolder, assetId)
        {
            Model = model;
            SerialNumber = serialNumber;
        }
    }
}