using System;
using System.Collections.Generic;
using System.IO;

namespace AssetManagement.Core.DataLoadStrategy
{
    public abstract class CsvLoaderBase<T> : IAssetLoadStrategy<T>
    {
        protected abstract uint MaxFileReadAttempts { get; }
        
        protected char Separator;

        /// <summary>
        /// Checks if a file is locked and cannot be read from.
        /// </summary>
        /// <returns>True if the file can be read, false if it is locked.</returns>
        protected bool IsFileReady(string filePath)
        {
            try
            {
                using (FileStream inputStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return inputStream.Length > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public abstract IEnumerable<T> ReadData(string filePath);
    }
}