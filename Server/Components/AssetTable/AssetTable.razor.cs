using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.Server.Shared;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

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

        private bool showModelColumn = true;
        private bool showOwnerColumn = true;
        private bool showPcAdStatusColumn = true;
        private bool showSerialNumberColumn = true;
        private bool showStateColumn = true;
        private bool showUsernameColumn = true;

        private string searchTerm;

        private async void OnSearchInput(ChangeEventArgs e)
        {
            searchTerm = e.Value.ToString().ToLower();
            
            IEnumerable<Computer> foundAssets = assets.Where(computer => AssetSearchAllPredicate(computer, searchTerm));
            
            if (!foundAssets.Any())
            {
                await MatDialogService.AlertAsync("Fandt ingen assets");
                pageAssets = navigator.OnItemsUpdated(assets);
                await InvokeAsync(StateHasChanged);
                searchTerm = "";
                return;
            }

            pageAssets = navigator.OnItemsUpdated(foundAssets.ToArray());
            await InvokeAsync(StateHasChanged);
        }

        private bool AssetSearchAllPredicate(Computer computer, string searchTerm)
        {
            if (computer.Id.ToLower().Contains(searchTerm)) return true;
            if (computer.AssetId.ToLower().Contains(searchTerm)) return true;
            if (computer.Model != null && computer.Model.ToLower().Contains(searchTerm)) return true;
            if (computer.SerialNumber != null && computer.SerialNumber.ToLower().Contains(searchTerm)) return true;
            if (computer.LastAssetRecord != null && computer.CurrentHolder != null)
            {
                if (computer.CurrentHolder.Username.ToLower().Contains(searchTerm)) return true;
                if (computer.CurrentHolder.Name.ToLower().Contains(searchTerm)) return true;
                if (computer.CurrentState.ToString().ToLower().Contains(searchTerm)) return true;
            }

            return false;
        }

        private void FilterMenuShow(MouseEventArgs e)
        {
            Menu.OpenAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            assets = FetchAllAssets();
            navigator = new PageNavigator<Computer>(assets, out pageAssets, AssetsPerPage);
            
            navigator.PageChanged += GetPageAssets;
            ComputerService.AssetUpdated += OnAssetUpdated;
        }

        private Computer[] FetchAllAssets() => ComputerService.GetAssets();

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
            await JSRuntime.InvokeAsync<object>("open", new object[] { url, "_blank" });
        }

        // TODO: Add updating of asset id when we find it in a file by serialnumber
        private async void AddNewAsset()
        {
            string result = await MatDialogService.PromptAsync("Tilføj et nyt asset vha. serienummer:");

            if (string.IsNullOrWhiteSpace(result))
            {
                return;
            }

            Computer asset = new Computer(result) {PcName = "-"};
            ComputerService.AddAsset(asset);

            bool assetIsInDatabase = ComputerService.GetAssetBySerialNumber(asset.SerialNumber) != null;
            
            if (assetIsInDatabase)
            {
                await MatDialogService.AlertAsync("Asset tilføjet.");
            }
            else
            {
                await MatDialogService.AlertAsync("Asset blev ikke tilføjet.");
            }
        }
    }
}