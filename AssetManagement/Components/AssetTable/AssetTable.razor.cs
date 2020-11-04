using System.Threading.Tasks;
using AssetManagement.Core;

namespace Components
{
    public partial class AssetTable
    {
        private IAsset[] assets;
        private IAsset[] pageAssets;
        
        private TableNavigator navigator;

        protected override async Task OnInitializedAsync()
        {
            await GetAssetAsync();
            
            navigator = new TableNavigator(assets, out pageAssets);
            navigator.OnPageChanged += GetPageAssets;
        }

        /// <summary>
        /// Deletes single asset entitiy.
        /// </summary>
        /// <param name="asset">List of all assets</param>
        private async Task DeleteAsset(IAsset asset)
        {
            await AssetService.DeleteAsset(asset);
            await GetAssetAsync();
        }
        
        /// <summary>
        /// Gets all assets from database.
        /// </summary>
        private async Task GetAssetAsync() => assets = await AssetService.GetAssetsAsync();

        /// <summary>
        /// Callback function fired when the page is changed. Updates the pageAssets array.
        /// </summary>
        /// <param name="pageAssets">Sliced array of assets representing a page.</param>
        private void GetPageAssets(IAsset[] pageAssets) => this.pageAssets = pageAssets;
    }
}
