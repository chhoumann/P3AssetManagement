using System;
using System.Collections.Generic;
using System.IO;
using AssetManagement.Models.DataLoadStrategy;

namespace AssetManagement.Models
{
    /// <summary>
    ///     Watches directory for new files and reacts when a new file is found.
    /// </summary>
    /// <typeparam name="TOut">Type to return data as.</typeparam>
    public abstract class AafFileWatcherBase<TOut>
    {
        private readonly string directoryPath;
        private readonly string fileTypeFilter;
        protected IAssetLoadStrategy<TOut> LoadStrategy;

        /// <summary>
        ///     Constructor for AafFileWatcherBase. Sets necessary fields.
        /// </summary>
        /// <param name="directoryPath">Path to directory.</param>
        /// <param name="fileTypeFilter">File type to filter for in folder.</param>
        /// <param name="loadStrategy">Strategy for loading the specific file type.</param>
        protected AafFileWatcherBase(string directoryPath, string fileTypeFilter,
            IAssetLoadStrategy<TOut> loadStrategy)
        {
            this.directoryPath = directoryPath;
            this.fileTypeFilter = fileTypeFilter;
            LoadStrategy = loadStrategy;
        }

        public abstract event Action<List<TOut>> FileRead;

        /// <summary>
        ///     Start watching for new CSV files at the path the class was initialized with.
        /// </summary>
        public AafFileWatcherBase<TOut> StartWatching()
        {
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = directoryPath,
                NotifyFilter = NotifyFilters.FileName,
                Filter = fileTypeFilter
            };

            watcher.Created += OnNewFile;
            watcher.Error += OnError;

            watcher.EnableRaisingEvents = true;

            return this;
        }

        /// <summary>
        ///     Fired when a new file is created.
        /// </summary>
        private protected abstract void OnNewFile(object eventSource, FileSystemEventArgs e);

        /// <summary>
        ///     Reads new data file.
        /// </summary>
        /// <param name="filePath">Path to directory.</param>
        /// <returns>IEnumerable of read data.</returns>
        private protected abstract IEnumerable<TOut> ReadNewDataFile(string filePath);

        /// <summary>
        ///     Fired when the FileSystemWatcher errors.
        /// </summary>
        private protected abstract void OnError(object sender, ErrorEventArgs e);
    }
}