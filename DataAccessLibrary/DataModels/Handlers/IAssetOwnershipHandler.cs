namespace AssetManagement.DataAccessLibrary.DataModels.Handlers
{
    public interface IAssetOwnershipHandler
    {
        void ToDepot();
        void ToCage();
        void ToUser(AssetHolder user);
    }
}