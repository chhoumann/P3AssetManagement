namespace AssetManagement.Core
{
    /// <summary>
    /// Interface for an employee which derrives IAssetHolder.
    /// </summary>
    public interface IEmployee : IAssetHolder
    {
        string Username { get; }
        string Department { get; }
    }
}