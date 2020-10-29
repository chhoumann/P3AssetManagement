namespace AssetManagement.Core
{
    /// <summary>
    /// General interface for all types of asset holders.
    /// </summary>
    public interface IAssetHolder
    {
        // Corresponds to name for an employee or the display label for the cage or depot
        string Label { get; } 
    }
}