using AssetManagement.Core;
using System;

namespace Components
{
    public partial class AssetTable
    {
        private class TableNavigator
        {
            public int PageIndex { get; private set; } = 0;
            public int MaxPages { get; private set; } = 0;

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
                pageAssets = GetPage(assets);
                CalculateMaxPages(assets.Length);
            }

            /// <summary>
            /// Changes the shown assets depending on which direction is chosen and invokes the event OnPageChanged.
            /// </summary>
            /// <param name="assets">The array of all assets.</param>
            /// <param name="navigationDirection">The direction to navigate - left or right.</param>
            public void ChangePage(IAsset[] assets, HorizontalDirection navigationDirection)
            {
                int direction = (int) navigationDirection;
                int numPages = CalculateMaxPages(assets.Length);
                int requestedPageIndex = PageIndex + direction;

                if (requestedPageIndex >= numPages || requestedPageIndex < 0) return;

                PageIndex += direction;
                OnPageChanged?.Invoke(GetPage(assets));
            }

            private int CalculateMaxPages(int assetCount) => MaxPages = assetCount / AssetsPerPage;

            /// <summary>
            /// Slices asset array into a size of AssetsPerPage and returns them as an IAsset array.
            /// </summary>
            /// <param name="assets">Array of all the assets in the system.</param>
            /// <returns>Array of assets given the current page.</returns>
            private IAsset[] GetPage(IAsset[] assets)
            {
                IAsset[] pageAssets = new IAsset[AssetsPerPage];

                for (int i = 0; i < pageAssets.Length; i++)
                {
                    pageAssets[i] = assets[PageIndex * AssetsPerPage + i];
                }

                return pageAssets;
            }
        }
    }
}
