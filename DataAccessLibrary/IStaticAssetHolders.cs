using AssetManagement.DataAccessLibrary.DataModels;

namespace AssetManagement.DataAccessLibrary
{
    public interface IStaticAssetHolders
    {
        AssetHolder Depot { get; }
        AssetHolder Cage { get; }
    }
}