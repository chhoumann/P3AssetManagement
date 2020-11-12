namespace AssetManagement.DataAccessLibrary
{
    public sealed partial class SqlDataAccess
    {
        public AssetDataAccess Asset { get; }
        public AssetRecordDataAccess AssetRecord { get; }

        public SqlDataAccess(AssetContext db)
        {
            Asset = new AssetDataAccess(db);
            AssetRecord = new AssetRecordDataAccess(db);
        }
    }
}
