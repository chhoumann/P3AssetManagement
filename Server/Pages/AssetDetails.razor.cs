using System;
using System.Threading.Tasks;
using AssetManagement.Models.Asset;
using AssetManagement.Models.AssetRecord;
using AssetManagement.Server.Shared;
using Microsoft.AspNetCore.Components;

namespace AssetManagement.Server.Pages
{
    public sealed partial class AssetDetails
    {
        [Parameter] public int AssetId { get; set; }

        private IAsset asset;
        private IAssetRecord[] assetRecords;
        private IAssetRecord[] pageAssetRecords;

        private IPageNavigator<IAssetRecord> navigator;

        private const int AssetRecordsPerPage = 10;

        private readonly string[] dialogOptions = new[]
        {
            "Ja", "Annuller"
        };

        protected override async Task OnInitializedAsync()
        {
            asset = await AssetService.GetSingleAssetAsync(AssetId);

            assetRecords = asset.AssetRecords.ToArray();

            navigator = new PageNavigator<IAssetRecord>(assetRecords, out pageAssetRecords, AssetRecordsPerPage);
            navigator.PageChanged += GetAssetRecords;
        }

        private void GetAssetRecords(IAssetRecord[] pageAssetRecords)
        {
            this.pageAssetRecords = pageAssetRecords;
        }

        private async Task DeleteAsset()
        {
            await AssetService.DeleteAsset(asset.Id);
            await JSRuntime.InvokeAsync<object>("close", new object[] { });
        }

        private async Task DeleteAssetPrompt()
        {
            string result = await MatDialogService.AskAsync($"Er du sikker på, at du vil slette {asset.AssetId}?", dialogOptions);
            
            if (DidUserClickConfirm(result))
            {
                await DeleteAsset();
            }
        }

        private async Task MoveAssetToDepotPrompt()
        {
            string result = await MatDialogService.AskAsync($"Er du sikker på, at du vil flytte {asset.AssetId} til depotet?", dialogOptions);
            
            if (DidUserClickConfirm(result))
            {
                asset.Transfer.ToDepot();
            }
        }

        private async Task MoveAssetToCagePrompt()
        {
            string result = await MatDialogService.AskAsync($"Er du sikker på, at du vil sende {asset.AssetId} til bortskaffelse?", dialogOptions);
            
            if (DidUserClickConfirm(result))
            {
                asset.Transfer.ToCage();
            }
        }

        private bool DidUserClickConfirm(string result) => result == dialogOptions[0];
    }
}
