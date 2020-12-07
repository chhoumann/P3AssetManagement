using System;
using System.Collections.Generic;
using System.IO;
using AssetManagement.Models.DataLoadStrategy;

namespace AssetManagement.Core.DataLoadStrategy
{
    public abstract class CsvLoaderBase<T> : IAssetLoadStrategy<T>
    {
        protected char Separator;
        protected abstract uint MaxFileReadAttempts { get; }

        public abstract IEnumerable<T> ReadData(string filePath);

        /// <summary>
        ///     Checks if a file is locked and cannot be read from.
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
    }
}