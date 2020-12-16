using AssetManagement.DataAccessLibrary;
using AssetManagement.DataAccessLibrary.DataModels;
using MatBlazor;

namespace AssetManagement.Server.Components.AssetTable
{
    public partial class AddAssetButton
    {
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