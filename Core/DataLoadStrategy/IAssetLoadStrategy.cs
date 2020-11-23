namespace AssetManagement.Core.DataLoadStrategy
{
    public interface IAssetLoadStrategy<out T>
    {
        T ReadAssetFromCsvLine(string csvLine, string filePath);
    }
}