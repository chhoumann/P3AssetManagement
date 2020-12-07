using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AssetManagement.Core;
using AssetManagement.Core.DataLoadStrategy;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.Models;
using NUnit.Framework;

namespace AssetManagement.NUnitTests.IntegrationTests
{
    public class AssetControllerIntegrationTests
    {
        
        string filePathToBFolder = Path.Combine(Environment.CurrentDirectory, 
            "test-files", "file-watcher-test", "toB");
        
        string filePathFromA = Path.Combine(Environment.CurrentDirectory, 
                                "test-files", "file-watcher-test", "fromA", "2020-10-07-PCID.csv");
        string filePathToB = Path.Combine(Environment.CurrentDirectory, 
            "test-files", "file-watcher-test", "toB", "2020-10-07-PCID.csv");
        
        // Succes: Vi har en funktion som subscriber til FileRead fra AafComputerCsvFileWatcher, der bliver invoket.
        [Test]
        public void METHOD()
        {
            Console.WriteLine(filePathToB);
            // Arrange
            List<Computer> computerAssets = new List<Computer>();
            bool hasBeenCallled = false;

            new AafComputerCsvFileWatcher(filePathToBFolder, new ComputerDataCsvLoader(';'))
                .StartWatching()
                .FileRead += (output) =>
            {
                hasBeenCallled = true;
            };
            
            
            // Act
            if (File.Exists(filePathToB))
            {
                File.Delete(filePathToB);
            }
            File.Copy(filePathFromA,filePathToB);

            // Assert
            Assert.That(() => hasBeenCallled, Is.True.After(5).Seconds.PollEvery(500).MilliSeconds);
        }
    }
}