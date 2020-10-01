using System;
using System.Collections.Generic;

namespace AssetManagement.Models
{
    public abstract class AssetHolder
    {
        public List<Asset> Assets { get; }

        public virtual void RecieveAsset(Asset asset) => Assets.Add(asset);

        public virtual void RemoveAsset(Asset asset)
        {
            if (Assets.Contains(asset))
            {
                Assets.Remove(asset);
            }
            else
            {
                Console.WriteLine($"Asset {asset.Name} with ID {asset.Id} not found");
            }
        }
    }
}