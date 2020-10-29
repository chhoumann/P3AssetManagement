namespace AssetManagement.Core
{
    public interface IAssetHolder
    {
        // Corresponds to name for an employee or the display label for the cage or depot
        string Label { get; } 
    }

    public interface IEmployee : IAssetHolder
    {
        string Username { get; }
        string Department { get; }
    }
}