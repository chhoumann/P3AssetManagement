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
            await GetAssetAsync();
            
            navigator = new PageNavigator<Asset>(assets, out pageAssets, AssetsPerPage);
            navigator.OnPageChanged += GetPageAssets;
        }
        
        /// <summary>
        /// Gets all assets from database.
        /// </summary>
        private async Task GetAssetAsync() => assets = await AssetService.GetAssetsAsync();

        /// <summary>
        /// Callback function fired when the page is changed. Updates the pageAssets array.
        /// </summary>
        /// <param name="pageAssets">Sliced array of assets representing a page.</param>
        private void GetPageAssets(Asset[] pageAssets) => this.pageAssets = pageAssets;
    }
}
