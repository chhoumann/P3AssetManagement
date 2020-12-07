using System.Collections.Generic;

namespace AssetManagement.Models.DataLoadStrategy
{
    public interface IAssetLoadStrategy<out T>
    {
        IEnumerable<T> ReadData(string filePath);
    }
}