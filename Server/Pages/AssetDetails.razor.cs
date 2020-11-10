using System.Threading.Tasks;
using AssetManagement.Core;
using Microsoft.AspNetCore.Components;

namespace AssetManagement.Server.Pages
{
    public sealed partial class AssetDetails
    {
        [Parameter] public int AssetId { get; set; }
        public IAsset Asset { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Asset = await AssetService.GetSingleAssetAsync(AssetId);
        }
    }
}
