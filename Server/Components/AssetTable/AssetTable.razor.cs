using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.Server.Shared;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Server.Components.AssetTable
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
        private bool showModel1Column = true;
        private bool showModel2Column = true;
        private bool showOwnerColumn = true;
        private bool showPcAdStatusColumn = true;
        private bool showSerialNumberColumn = true;
        private bool showStateColumn = true;
        private bool showUsernameColumn = true;

        private string searchTerm;

        protected override async Task OnInitializedAsync()
        {
            assets = FetchAllAssets();
            navigator = new PageNavigator<Computer>(assets, out pageAssets, AssetsPerPage);

            navigator.PageChanged += GetPageAssets;
            ComputerService.AssetUpdated += OnAssetUpdated;
        }

        private Computer[] FetchAllAssets() => ComputerService.GetAssets();

        /// <summary>
        ///     Callback function fired when the page is changed. Updates the pageAssets array.
        /// </summary>
        /// <param name="assetsOnPage">Sliced array of assets representing a page.</param>
        private void GetPageAssets(Computer[] assetsOnPage) => pageAssets = assetsOnPage;

        /// <summary>
        ///     Callback for AssetUpdate event that occurs in AssetService.
        ///     Updates table with new data - live reloading.
        /// </summary>
        private async void OnAssetUpdated()
        {
            assets = FetchAllAssets();
            pageAssets = navigator.OnItemsUpdated(assets);

            await InvokeAsync(StateHasChanged);
        }

        private async void OnSearchInput(ChangeEventArgs e)
        {
            searchTerm = e.Value.ToString()?.ToLower();

            await SearchHandler();
        }

        private async Task SearchHandler()
        {
            Computer[] foundAssets = assets.Where(computer => AssetSearchAllPredicate(computer, searchTerm)).ToArray();

            if (!foundAssets.Any())
            {
                searchTerm = "";
                await MatDialogService.AlertAsync("Fandt ingen assets");
                pageAssets = navigator.OnItemsUpdated(assets);

                await InvokeAsync(StateHasChanged);
                return;
            }

            pageAssets = navigator.OnItemsUpdated(foundAssets);
        }

        private bool AssetSearchAllPredicate(Computer computer, string query)
        {
            if (computer.Id.ToLower().Contains(query)) return true;
            if (computer.PcAdStatus != null && computer.PcAdStatus.ToLower().Contains(query)) return true;
            if (computer.AssetId.ToLower().Contains(query)) return true;
            if (computer.Models != null &&
                computer.Models.Any(model => model.Name.ToLower().Contains(query))) return true;
            if (computer.SerialNumber != null && computer.SerialNumber.ToLower().Contains(query)) return true;
            if (computer.LastAssetRecord != null && computer.CurrentHolder != null)
            {
                if (computer.CurrentHolder.Username.ToLower().Contains(query)) return true;
                if (computer.CurrentHolder.Name.ToLower().Contains(query)) return true;
                if (computer.CurrentState.ToString().ToLower().Contains(query)) return true;
            }

            return false;
        }

        private void FilterMenuShow(MouseEventArgs e)
        {
            Menu.OpenAsync();
        }
    }
}