using AssetManagement.Core;

namespace AssetManagement.Models
{
    /// <summary>
    /// A type of IAssetHolder which functions as a simple container for IAssets.
    /// </summary>
    public sealed class Depot : IAssetHolder
    {
        public string Label => "Depot";
    }
}