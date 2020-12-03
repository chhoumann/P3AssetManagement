using System.Collections.Generic;

namespace AssetManagement.Core.DataLoadStrategy
{
    public interface IAssetLoadStrategy<out T>
    {
        IEnumerable<T> ReadData();
    }
}