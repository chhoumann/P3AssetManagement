using AssetManagement.Core;

namespace AssetManagement.Models
{
    public sealed class AssetHolder : IAssetHolder
    {
        public string Label { get; }

        public string Username { get; }

        public string Department { get; }
        
        public AssetHolder(string label, string username, string department)
        {
            Label = label;
            Username = username;
            Department = department;
        }
    }
}
