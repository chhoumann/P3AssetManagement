namespace AssetManagement.Core
{
    public interface IEmployee : IAssetHolder
    {
        string Username { get; }
        string Department { get; }
    }
}