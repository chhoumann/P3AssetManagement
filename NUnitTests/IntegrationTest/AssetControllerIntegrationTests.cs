using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AssetManagement.Core;
using AssetManagement.Core.DataLoadStrategy;
using AssetManagement.DataAccessLibrary;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using AssetManagement.Models;
using AssetManagement.Models.DataLoadStrategy;
using NUnit.Framework;

namespace AssetManagement.NUnitTests.IntegrationTests
{
    public sealed class MockComputerService : IAssetService<Computer>
    {
        public event Action AssetUpdated;
        public Computer[] GetAssets() => throw new NotImplementedException();

        public Computer GetAssetById(string id) => throw new NotImplementedException();

        public void AddAsset(Computer asset)
        {
            throw new NotImplementedException();
        }

        public void AddAssets(IEnumerable<Computer> assets)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsset(Computer asset)
        {
            throw new NotImplementedException();
        }
    }
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
        public void AafComputerCsvFileWatcher_NewDataReceived_FileReadEventIsCalled()
        {
            // Arrange
            bool hasBeenCallled = false;

            AafFileWatcherBase<Computer> fileWatcher = 
                new AafComputerCsvFileWatcher(filePathToBFolder, new ComputerCsvLoader(';'))
                .StartWatching();
                
            // Act
            fileWatcher.FileRead += computers => hasBeenCallled = true;
            
            if (File.Exists(filePathToB))
            {
                File.Delete(filePathToB);
            }
            File.Copy(filePathFromA,filePathToB);

            // Assert
            Assert.That(() => hasBeenCallled, Is.True.After(5).Seconds.PollEvery(500).MilliSeconds);
        }

        [Test]
        public void AssetComparer_AssetOwnerChangedOnNewData_HolderUpdated()
        {
            // Arrange
            AssetHolder assetHolder1 = new AssetHolder();
            AssetHolder assetHolder2 = new AssetHolder();
            
            List<IAsset> currentAssets = new List<IAsset> { new Computer() {PcName = "PC1"} };
            List<IAsset> assetsFromCsvList = new List<IAsset> { new Computer() {PcName = "PC1"} };
            
            AssetComparer<IAsset> assetComparer = new AssetComparer<IAsset>(currentAssets);
            
            assetsFromCsvList[0].Transfer.ToUser(assetHolder1);
            currentAssets[0].Transfer.ToUser(assetHolder1);

            // Act
            assetsFromCsvList[0].Transfer.ToUser(assetHolder2);
            
            assetComparer.OnNewData(assetsFromCsvList);

            // Assert
            Assert.That(currentAssets[0].LastAssetRecord.Holder, Is.EqualTo(assetHolder2));
        }

        [Test]
        public void Method()
        {
            // Arrange
            AafComputerCsvFileWatcher fileWatcher = new AafComputerCsvFileWatcher(filePathToBFolder, new ComputerCsvLoader(';'));
            AssetController<Computer, MockComputerService> assetController = new AssetController<Computer, MockComputerService>()
                .StartWatchingAlienData(fileWatcher);
            
            // Act
            
            if (File.Exists(filePathToB))
            {
                File.Delete(filePathToB);
            }
            File.Copy(filePathFromA,filePathToB);
            
            // Assert
            
        }
    }
}