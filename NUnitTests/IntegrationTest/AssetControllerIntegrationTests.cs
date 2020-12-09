using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssetManagement.DataAccessLibrary;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using AssetManagement.Models;
using AssetManagement.Models.DataLoadStrategy;
using NUnit.Framework;

namespace AssetManagement.NUnitTests.IntegrationTest
{
    public sealed class MockComputerService : IAssetService<Computer>
    {
        public static List<Computer> computersRecievedFromController = new List<Computer>();
        public event Action AssetUpdated;
        public AssetHolder Cage { get; }
        public AssetHolder Depot { get; }

        public Computer[] GetAssets() => new ComputerCsvLoader(';').ReadData(Path.Combine(Environment.CurrentDirectory, 
            "test-files", "asset-controller-test", "2020-10-07-PCID.csv")).ToArray();

        public Computer GetAssetById(string id) => throw new NotImplementedException();

        public void AddAsset(Computer asset)
        {
            throw new NotImplementedException();
        }

        public void AddAssets(IEnumerable<Computer> assets)
        {
            computersRecievedFromController.AddRange(assets);
        }

        public void DeleteAsset(Computer asset)
        {
            throw new NotImplementedException();
        }
    }
    public class AssetControllerIntegrationTests
    {
        private const int FileReadTimeout = 5;
        
        // Filepaths for FileWatcher test
        private readonly string filePathToBFolder = Path.Combine(Environment.CurrentDirectory, 
            "test-files", "file-watcher-test", "toB");
        
        private readonly string filePathFromA = Path.Combine(Environment.CurrentDirectory, 
            "test-files", "file-watcher-test", "fromA", "2020-10-07-PCID.csv");
        private readonly string filePathToB = Path.Combine(Environment.CurrentDirectory, 
            "test-files", "file-watcher-test", "toB", "2020-10-07-PCID.csv");
        
        // Filepaths for AssetController test
        private readonly string filePathToDFolder = Path.Combine(Environment.CurrentDirectory, 
            "test-files", "asset-controller-test", "toD");
        
        private readonly string filePathFromC = Path.Combine(Environment.CurrentDirectory, 
            "test-files", "asset-controller-test", "fromC", "2020-10-27-PCID.csv");
        private readonly string filePathToD = Path.Combine(Environment.CurrentDirectory, 
            "test-files", "asset-controller-test", "toD", "2020-10-27-PCID.csv");
        
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
            Assert.That(() => hasBeenCallled, Is.True.After(FileReadTimeout).Seconds.PollEvery(500).MilliSeconds);
        }

        [Test]
        public void AssetComparer_AssetOwnerChangedOnNewData_HolderUpdated()
        {
            // Arrange
            AssetHolder assetHolder1 = new AssetHolder();
            AssetHolder assetHolder2 = new AssetHolder();
            
            List<IAsset> currentAssets = new List<IAsset> { new Computer() {PcName = "PC1"} };
            List<IAsset> assetsFromCsvList = new List<IAsset> { new Computer() {PcName = "PC1"} };
            
            assetsFromCsvList[0].Transfer.ToUser(assetHolder1);
            currentAssets[0].Transfer.ToUser(assetHolder1);
            
            AssetComparer<IAsset> assetComparer = new AssetComparer<IAsset>(currentAssets);

            // Act
            assetsFromCsvList[0].Transfer.ToUser(assetHolder2);
            
            assetComparer.OnNewData(assetsFromCsvList);

            // Assert
            Assert.That(currentAssets[0].LastAssetRecord.Holder, Is.EqualTo(assetHolder2));
        }
        
        [Test]
        public void AssetController_NewDataRecievedWithOneNewUser_OneAssetAddedToIAssetServices()
        {
            // Arrange
            AafComputerCsvFileWatcher fileWatcher = 
                new AafComputerCsvFileWatcher(filePathToDFolder, new ComputerCsvLoader(';'));
            new AssetController<Computer, MockComputerService>().StartWatchingAlienData(fileWatcher);
            
            // Act
            if (File.Exists(filePathToD))
            {
                File.Delete(filePathToD);
            }
            File.Copy(filePathFromC,filePathToD);
            
            // Assert
            Assert.That(() => (MockComputerService.computersRecievedFromController.Count == 1), 
                Is.True.After(FileReadTimeout).Seconds.PollEvery(500).MilliSeconds);
        }
    }
}