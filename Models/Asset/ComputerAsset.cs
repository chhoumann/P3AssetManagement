namespace AssetManagement.Models.Asset
{
    public class ComputerAsset : Asset
    {
        public string Model { get; private set; }
        public string SerialNumber { get; private set; }
        
        public ComputerAsset(string model, string serialNumber) : base()
        {
            Model = model;
            SerialNumber = serialNumber;
        }
        
        public ComputerAsset(string assetId, string model, string serialNumber) : base(assetId)
        {
            Model = model;
            SerialNumber = serialNumber;
        }
    }
}