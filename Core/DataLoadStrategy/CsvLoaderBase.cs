using System;
using System.Collections.Generic;
using System.IO;

namespace AssetManagement.Core.DataLoadStrategy
{
    public abstract class CsvLoaderBase<T> : IAssetLoadStrategy<T>
    {
        protected abstract uint MaxFileReadAttempts { get; }
        
        protected char Separator;
        protected string FilePath;

        /// <summary>
        /// Checks if a file is locked and cannot be read from.
        /// </summary>
        /// <returns>True if the file can be read, false if it is locked.</returns>
        protected bool IsFileReady()
        {
            try
            {
                using (FileStream inputStream = File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return inputStream.Length > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public abstract IEnumerable<T> ReadData();
    }
}