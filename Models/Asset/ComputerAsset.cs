namespace AssetManagement.Models.Asset
{
    public class ComputerAsset : Asset
    {
        public string Model { get; private set; }
        public string SerialNumber { get; private set; }
        
        public ComputerAsset(int id, string model, string serialNumber) : base(id)
        {
            Model = model;
            SerialNumber = serialNumber;
        }
        
        public ComputerAsset(string assetId, int id, string model, string serialNumber) : base(assetId, id)
        {
            Model = model;
            SerialNumber = serialNumber;
        }
    }
}