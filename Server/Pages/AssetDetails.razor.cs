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

        private IAsset asset { get; set; }
        private IAssetRecord[] assetRecords;
        private IAssetRecord[] pageAssetRecords;

        private PageNavigator<IAssetRecord> navigator;

        private const int AssetsPerPage = 10;

        protected override async Task OnInitializedAsync()
        {
            asset = await AssetService.GetSingleAssetAsync(AssetId);

            assetRecords = asset.AssetRecords.ToArray();

            navigator = new PageNavigator<IAssetRecord>(assetRecords, out pageAssetRecords, AssetsPerPage);
            navigator.OnPageChanged += GetAssetRecords;
        }

        private void GetAssetRecords(IAssetRecord[] pageAssetRecords)
        {
            this.pageAssetRecords = pageAssetRecords;
        }
    }
}
