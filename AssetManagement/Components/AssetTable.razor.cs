using System.Threading.Tasks;
using AssetManagement.Models;


namespace Components
{
    public partial class AssetTable
    {
        private Asset[] assets;

        protected override async Task OnInitializedAsync()
        {
            assets = await AssetService.GetAssetsAsync();
        }
    }
}