using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssetManagement.Core.DataLoadStrategy
{
    public sealed class AssetLoadCsvContext<T>
    {
        private IAssetLoadStrategy<T> assetLoadStrategy;
        
        public AssetLoadCsvContext(IAssetLoadStrategy<T> assetLoadStrategy) => this.assetLoadStrategy = assetLoadStrategy;

        /// <summary>
        /// Replace strategy object at runtime
        /// </summary>
        /// <param name="assetLoadStrategy">Strategy given by client</param>
        public void SetStrategy(IAssetLoadStrategy<T> assetLoadStrategy)
        {
            this.assetLoadStrategy = assetLoadStrategy;
        }
        
        /// <summary>
        /// Iterates through a csv file and makes a List<T> based upon the chosen AssetLoadStrategy.
        /// </summary>
        /// <param name="filePath">The filepath for the csv-file</param>
        /// <returns></returns>
        public List<T> LoadData(string filePath)
        {
            return File
                .ReadAllLines(filePath)
                .Skip(1)
                .Select(csvLine => assetLoadStrategy.ReadAssetFromCsvLine(csvLine, filePath))
                .ToList();
        }
    }
}