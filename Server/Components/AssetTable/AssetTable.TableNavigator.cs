using AssetManagement.Core;
using System;
using System.Collections.Generic;

namespace AssetManagement.Server.Components
{
    public partial class AssetTable
    {
        private sealed class TableNavigator
        {
            public int PageIndex { get; private set; } = 0;
            public int NumPages { get; private set; } = 0;

            /// <summary>
            /// Event that fires when the page changes.
            /// </summary>
            public event Action<IAsset[]> OnPageChanged;

            private const int AssetsPerPage = 10;

            /// <summary>
            /// Constructor to initialize TableNavigator which outputs pageAssets.
            /// </summary>
            /// <param name="assets">Input array with all assets.</param>
            /// <param name="pageAssets">Output array with sliced assets of size AssetsPerPage.</param>
            public TableNavigator(IAsset[] assets, out IAsset[] pageAssets)
            {
                CalculateMaxPages(assets.Length);
                pageAssets = GetPage(assets);
            }

            /// <summary>
            /// Changes the shown assets depending on which direction is chosen and invokes the event OnPageChanged.
            /// </summary>
            /// <param name="assets">The array of all assets.</param>
            /// <param name="navigationDirection">The direction to navigate - left or right.</param>
            public void ChangePage(IAsset[] assets, HorizontalDirection navigationDirection)
            {
                int direction = (int)navigationDirection;
                int requestedPageIndex = PageIndex + direction;
                
                if (requestedPageIndex >= NumPages || requestedPageIndex < 0) return;

                PageIndex += direction;
                OnPageChanged?.Invoke(GetPage(assets));
            }

            // Should be hooked up to an event when the server asset database is updated
            private void CalculateMaxPages(int assetCount) => NumPages = assetCount / AssetsPerPage;

            /// <summary>
            /// Slices asset array into a size of AssetsPerPage and returns them as an IAsset array.
            /// </summary>
            /// <param name="assets">Array of all the assets in the system.</param>
            /// <returns>Array of assets given the current page.</returns>
            private IAsset[] GetPage(IAsset[] assets)
            {
                List<IAsset> pageAssets = new List<IAsset>();

                int start = PageIndex * AssetsPerPage;
                int end = Math.Clamp(start + AssetsPerPage, 0, assets.Length);

                for (int i = start; i < end; i++)
                {
                    pageAssets.Add(assets[i]);
                }

                return pageAssets.ToArray();
            }
        }
    }
}
