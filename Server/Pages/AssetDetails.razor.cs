using System;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.DataAccessLibrary;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using AssetManagement.Server.Shared;
using Microsoft.AspNetCore.Components;

namespace AssetManagement.Server.Pages
{
    // TODO: Use interchangeable AssetService instead of ComputerService
    public sealed partial class AssetDetails
    {
        private const int AssetRecordsPerPage = 10;

        private readonly string[] dialogOptions =
        {
            "Ja", "Annuller"
        };

        private Computer asset;

        private IPageNavigator<IAssetRecord> navigator;

        private IAssetRecord[] pageAssetRecords;
        [Parameter] public Guid AssetDbId { get; set; }
        private IAssetRecord[] AssetRecords => asset.AssetRecords.OrderByDescending(x => x.Timestamp).ToArray();

        protected override async Task OnInitializedAsync()
        {
            asset = ComputerService.GetAssetById(AssetDbId.ToString());

            navigator = new PageNavigator<IAssetRecord>(AssetRecords, out pageAssetRecords, AssetRecordsPerPage);
            navigator.PageChanged += GetAssetRecords;
        }

        private void GetAssetRecords(IAssetRecord[] pageAssetRecords)
        {
            this.pageAssetRecords = pageAssetRecords;
        }

        private async Task DeleteAssetPrompt()
        {
            string dialogMessage = $"Er du sikker på, at du vil slette {asset.AssetId}?";
            string result = await MatDialogService.AskAsync(dialogMessage, dialogOptions);

            if (UserClickedConfirm(result))
            {
                ComputerService.DeleteAsset(asset);
                await JSRuntime.InvokeAsync<object>("close", new object[] { });
            }
        }

        private async Task MoveAssetToDepotPrompt()
        {
            string dialogMessage = $"Er du sikker på, at du vil flytte {asset.AssetId} til depotet?";
            string result = await MatDialogService.AskAsync(dialogMessage, dialogOptions);

            if (UserClickedConfirm(result))
            {
                asset.Transfer.ToUser(ComputerService.Depot);
            }
        }

        private async Task MoveAssetToCagePrompt()
        {
            string dialogMessage = $"Er du sikker på, at du vil sende {asset.AssetId} til bortskaffelse?";
            string result = await MatDialogService.AskAsync(dialogMessage, dialogOptions);

            if (UserClickedConfirm(result))
            {
                asset.Transfer.ToUser(ComputerService.Cage);
            }
        }

        private bool UserClickedConfirm(string result) => result == dialogOptions[0];
    }
}