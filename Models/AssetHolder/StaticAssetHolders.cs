namespace AssetManagement.Models.AssetHolder
{
    public static class StaticAssetHolders
    {
        public static IAssetHolder Cage { get; } = new AssetHolder("Bur", null, null);
        public static IAssetHolder Depot { get; } = new AssetHolder("Depot", null, null);
    }
}