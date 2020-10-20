using System.Collections.Generic;

namespace AssetManagement.Models
{
    public abstract class AssetHolder
    {
        public string Name { get; }
        public string Department { get; }

        /// <summary>
        /// The list of assets currently being held by this asset holder.
        /// </summary>
        public List<IAsset> CurrentAssets { get; } = new List<IAsset>();

        public AssetHolder(string name) => Name = name;
    }
}
