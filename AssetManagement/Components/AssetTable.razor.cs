using System.Threading.Tasks;
using AssetManagement.Core;

namespace Components
{
    public partial class AssetTable
    {
        private IAsset[] assets;

        protected override async Task OnInitializedAsync()
        {
            assets = await AssetService.GetAssetsAsync();
        }
    }
}
