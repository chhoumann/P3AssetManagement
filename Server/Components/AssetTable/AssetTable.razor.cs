using System;
using System.Threading.Tasks;
using AssetManagement.Models.Asset;
using AssetManagement.Server.Shared;

namespace AssetManagement.Server.Components
{
    public partial class AssetTable
    {
        private Asset[] assets;
        private Asset[] pageAssets;

        private const int AssetsPerPage = 10;

        private PageNavigator<Asset> navigator;

        protected override async Task OnInitializedAsync()
        {
            await GetAssetsAsync();
            
            navigator = new PageNavigator<Asset>(assets, out pageAssets, AssetsPerPage);
            navigator.PageChanged += GetPageAssets;
            AssetService.AssetUpdated += OnAssetUpdated;
        }

        /// <summary>
        /// Gets all assets from database.
        /// </summary>
        private async Task GetAssetsAsync() => assets = await AssetService.GetAssetsAsync();

        /// <summary>
        /// Callback for AssetUpdate event that occurs in AssetService.
        /// Updates table with new data - live reloading.
        /// </summary>
        private async void OnAssetUpdated()
        {
            await GetAssetsAsync();
            navigator.OnItemsUpdated(assets);
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Callback function fired when the page is changed. Updates the pageAssets array.
        /// </summary>
        /// <param name="pageAssets">Sliced array of assets representing a page.</param>
        private void GetPageAssets(Asset[] pageAssets) => this.pageAssets = pageAssets;

        /// <summary>
        /// Opens the details page for an asset in a new page.
        /// </summary>
        /// <param name="asset">IAsset to open details for</param>
        private async Task NavigateToDetails(IAsset asset)
        {
            string url = $"{NavigationManager.BaseUri}/AssetDetails/{asset.Id}";
            await JSRuntime.InvokeAsync<object>("open", new object[] { url, "_blank" });
        }
    }
}
