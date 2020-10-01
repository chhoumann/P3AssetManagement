﻿using System;
using System.Collections.Generic;

namespace AssetManagement.Models
{
    public abstract class AssetHolder
    {
        /// <summary>
        /// The list of assets currently being held by this asset holder.
        /// </summary>
        public List<Asset> CurrentAssets { get; }

        public virtual AssetHolder RecieveAsset(Asset asset)
        {
            if (CurrentAssets.Contains(asset))
            {
                // ERROR: we're already holding this asset!
                throw new ArgumentException("Attempt to add an asset to an asset holder's asset list which already contains this asset!", asset.Name);
            }

            CurrentAssets.Add(asset);
            return this;
        }

        public virtual void RemoveAsset(Asset asset)
        {
            if (CurrentAssets.Contains(asset))
            {
                CurrentAssets.Remove(asset);
            }
            else
            {
                // ERROR: The asset provided by the parameter does not exist in the list so we cannot remove it!
                throw new ArgumentException($"Attempt to remove asset \"{asset.Name}\" with ID {asset.Id} from an asset holder's asset list that did not contain the asset!");
            }
        }
    }
}