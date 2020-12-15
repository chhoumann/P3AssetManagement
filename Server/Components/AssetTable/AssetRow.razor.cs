using System.Threading.Tasks;
using AssetManagement.DataAccessLibrary.DataModels;
using Microsoft.AspNetCore.Components;

namespace AssetManagement.Server.Components.AssetTable
{
    public partial class AssetRow
    {
        [Parameter] public Computer Asset { get; set; }
        [Parameter] public bool ShowModel1Column { get; set; }
        [Parameter] public bool ShowModel2Column { get; set; }
        [Parameter] public bool ShowSerialNumberColumn { get; set; }
        [Parameter] public bool ShowIdColumn { get; set; }
        [Parameter] public bool ShowOwnerColumn { get; set; }
        [Parameter] public bool ShowUsernameColumn { get; set; }
        [Parameter] public bool ShowLastChangedColumn { get; set; }
        [Parameter] public bool ShowPcAdStatusColumn{ get; set; }
        [Parameter] public bool ShowStateColumn { get; set; }
            
        /// <summary>
        ///     Opens the details page for an asset in a new page.
        /// </summary>
        /// <param name="asset">IAsset to open details for</param>
        private async Task NavigateToDetails(Computer asset)
        {
            if (ComputerService.GetAssetById(asset.Id) == null)
            {
                await MatDialogService.AlertAsync("Asset does not exist.");
                return;
            }
        
            string url = $"{NavigationManager.BaseUri}AssetDetails/{asset.Id}";
            await JSRuntime.InvokeAsync<object>("open", new object[] {url, "_blank"});
        }
    }
}