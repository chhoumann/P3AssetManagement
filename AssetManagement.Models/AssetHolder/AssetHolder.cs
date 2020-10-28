using System;
using System.Collections.Generic;

namespace AssetManagement.Models
{
    public abstract class AssetHolder
    {
        public string Name { get; private set; }
        public string Department { get; private set; }
        public List<IAsset> AssetList; // Init here or in ctor?
        public AssetHolder(string name) => Name = name;

        /// <summary>
        /// The list of assets currently being held by this asset holder.
        /// </summary>
        public List<IAsset> CurrentAssets { get; } = new List<IAsset>();

        public virtual void RecieveAsset(IAsset asset)
        {
            if (CurrentAssets.Contains(asset))
            {
                // ERROR: we're already holding this asset!
                throw new ArgumentException("Attempt to add an asset to an asset holder's asset list which already contains this asset!", asset.Model);
            }

            CurrentAssets.Add(asset);
        }
    }
}
