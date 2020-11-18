using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AssetManagement.DataAccessLibrary.DataModels;

namespace AssetManagement.Core
{
    /// <summary>
    /// Factory controller responsible for creating assets, transferring assets and updating asset states.
    /// </summary>
    public sealed class AssetController
    {
        // Sole responsibility of this class should be to periodically update our assets.
        // Updating asset ownership is done by the asset itself (which calls an event to add record)
        
        public static void StartWatchingAlienData()
        {
            string filePath = Directory.GetParent(Environment.CurrentDirectory) + @"/AAFData";

            AafCsvDataWatcher aafCsvDataWatcher = new AafCsvDataWatcher(filePath);
            aafCsvDataWatcher.StartWatching();
        }

        public static List<ComputerData> GetComputerDataFromFile(string filePath, char separator)
        {
            return File
                .ReadAllLines(filePath)
                .Skip(1)
                .Select(x => new ComputerData(Path.GetFileName(filePath), separator, new CultureInfo("fr-FR"), x))
                .ToList();
        }
    }
}
