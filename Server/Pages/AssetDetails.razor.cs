using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using AssetManagement.Server.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Server.Pages
{
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
            string initialMessage = $"Er du sikker på, at du vil slette {asset.AssetId}?";
            string secondMessage = $"Er du helt sikker på, at du vil slette {asset.AssetId}?\n" +
                                  $"Advarsel! Denne handling er permanent og kan ikke fortrydes.";

            if (await RunPrompt(initialMessage) && await RunPrompt(secondMessage))
            {
                ComputerService.DeleteAsset(asset);
                ClosePage();
            }
        }

        private void ClosePage()
        {
            JSRuntime.InvokeAsync<object>("close", new object[] { });
        }

        /// <summary>
        /// Runs a prompt with the given message.
        /// </summary>
        /// <param name="message">The message which is shown to the user.</param>
        /// <returns>True if the user confirmed the action. False otherwise.</returns>
        private async Task<bool> RunPrompt(string message)
        {
            string result = await MatDialogService.AskAsync(message, dialogOptions);
            return UserClickedConfirm(result);
        }

        private bool UserClickedConfirm(string result) => result == dialogOptions[0];

        private async Task MoveAssetToDepotPrompt()
        {
            string dialogMessage = $"Er du sikker på, at du vil flytte {asset.AssetId} til depotet?";

            if (await RunPrompt(dialogMessage))
            {
                asset.Transfer.ToUser(ComputerService.Depot);
            }

            pageAssetRecords = navigator.OnItemsUpdated(AssetRecords);
        }

        private async Task MoveAssetToCagePrompt()
        {
            string dialogMessage = $"Er du sikker på, at du vil sende {asset.AssetId} til bortskaffelse?";

            if (await RunPrompt(dialogMessage))
            {
                asset.Transfer.ToUser(ComputerService.Cage);
            }

            pageAssetRecords = navigator.OnItemsUpdated(AssetRecords);
        }

    }
}