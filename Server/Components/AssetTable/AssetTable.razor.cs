using System.Threading.Tasks;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.Server.Shared;
using MatBlazor;
using Microsoft.AspNetCore.Components.Web;

namespace AssetManagement.Server.Components
{
    // TODO: Use interchangeable AssetService instead of ComputerService
    public partial class AssetTable
    {
        private const int AssetsPerPage = 9;
        private Computer[] assets;

        private ForwardRef buttonForwardRef = new ForwardRef();
        private BaseMatMenu Menu;

        private IPageNavigator<Computer> navigator;
        private Computer[] pageAssets;
        private bool showIdColumn = true;
        private bool showLastChangedColumn = true;

        private bool showModelColumn = true;
        private bool showOwnerColumn = true;
        private bool showPcAdStatusColumn = true;
        private bool showSerialNumberColumn = true;
        private bool showStateColumn = true;
        private bool showUsernameColumn = true;

        private void FilterMenuShow(MouseEventArgs e)
        {
            Menu.OpenAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            FetchAssets();
            navigator = new PageNavigator<Computer>(assets, out pageAssets, AssetsPerPage);
            navigator.PageChanged += GetPageAssets;
            ComputerService.AssetUpdated += OnAssetUpdated;
        }

        private void FetchAssets() => assets = ComputerService.GetAssets();

        /// <summary>
        ///     Callback for AssetUpdate event that occurs in AssetService.
        ///     Updates table with new data - live reloading.
        /// </summary>
        private async void OnAssetUpdated()
        {
            FetchAssets();
            pageAssets = navigator.OnItemsUpdated(assets);
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        ///     Callback function fired when the page is changed. Updates the pageAssets array.
        /// </summary>
        /// <param name="pageAssets">Sliced array of assets representing a page.</param>
        private void GetPageAssets(Computer[] pageAssets) => this.pageAssets = pageAssets;

        /// <summary>
        ///     Opens the details page for an asset in a new page.
        /// </summary>
        /// <param name="asset">IAsset to open details for</param>
        private async Task NavigateToDetails(Computer asset)
        {
            string url = $"{NavigationManager.BaseUri}AssetDetails/{asset.Id}";
            await JSRuntime.InvokeAsync<object>("open", new object[] {url, "_blank"});
        }
    }
}