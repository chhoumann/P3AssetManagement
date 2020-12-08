using System;
using System.Collections.Generic;
using System.Linq;
using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;

namespace AssetManagement.Models
{
    public sealed class AssetComparer<T> where T : IAsset
    {
        private readonly List<T> currentAssets;

        public AssetComparer(List<T> currentAssets) => this.currentAssets = currentAssets;
        public event Action<List<T>> NewAssetsFound;

        public void OnNewData(IEnumerable<T> assetsFromList)
        {
            IEnumerable<T> intersectingAssets = new List<T>();
            
            if (currentAssets.Count > 0)
            {
                intersectingAssets = GetIntersectingAssets(assetsFromList);
                
                UpdateAssets(intersectingAssets);
            }

            List<T> addedAssets = GetAddedAssets(assetsFromList, intersectingAssets);
            
            if (addedAssets.Count > 0)
            {
                NewAssetsFound?.Invoke(addedAssets);
            }
        }

        /// <summary>
        ///     Gets the intersecting assets between the current assets and the assets from the list.
        /// </summary>
        /// <param name="assetsFromList">The assets to compare to the current assets.</param>
        /// <returns>The intersecting assets between the current assets and the assets from the list.</returns>
        private IEnumerable<T> GetIntersectingAssets(IEnumerable<T> assetsFromList)
        {
            return assetsFromList
                .Where(newAsset => currentAssets
                    .Any(asset => newAsset.AssetId == asset.AssetId))
                .ToList();
        }

        private void UpdateAssets(IEnumerable<T> intersectingAssets)
        {
            UpdateAssetStates(intersectingAssets);
            UpdateAssetHolders(intersectingAssets);
        }

        /// <summary>
        ///     Updates the state of the assets' states to missing if they are no longer on the list.
        /// </summary>
        /// <param name="intersectingAssets">The intersecting assets between the current assets and the assets from the list.</param>
        private void UpdateAssetStates(IEnumerable<T> intersectingAssets)
        {
            foreach (T currentAsset in currentAssets)
            {
                if (!intersectingAssets.Any(asset => asset.AssetId == currentAsset.AssetId))
                {
                    currentAsset.ChangeState.ToMissing();
                }
                else if (currentAsset.LastAssetRecord == null ||
                         currentAsset.LastAssetRecord.State != AssetState.Online)
                {
                    currentAsset.ChangeState.ToOnline();
                }
            }
        }

        /// <summary>
        ///     Updates the asset holders of the assets that have changed holder.
        /// </summary>
        /// <param name="intersectingAssets">The intersecting assets between the current assets and the assets from the list.</param>
        private void UpdateAssetHolders(IEnumerable<T> intersectingAssets)
        {
            foreach (T newAsset in intersectingAssets)
            {
                T currentAsset = currentAssets.Find(oldAsset => oldAsset.AssetId == newAsset.AssetId);

                if (currentAsset.CurrentHolder == null ||
                    !currentAsset.CurrentHolder.Equals(newAsset.CurrentHolder))
                {
                    currentAsset.Transfer.ToUser(newAsset.CurrentHolder);
                }
            }
        }

        private List<T> GetAddedAssets(IEnumerable<T> newAssets, IEnumerable<T> intersectingAssets)
        {
            return newAssets
                .Where(addedAsset => intersectingAssets.All(asset => asset.AssetId != addedAsset.AssetId))
                .Select(addedAsset =>
                {
                    addedAsset.ChangeState.ToOnline();
                    return addedAsset;
                })
                .ToList();
        }
    }
}