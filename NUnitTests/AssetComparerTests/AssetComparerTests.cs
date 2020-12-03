using System.Collections.Generic;
using AssetManagement.Core;
using AssetManagement.Models.Asset;
using AssetManagement.Models.AssetRecord;
using NUnit.Framework;

namespace AssetManagement.NUnitTests.AssetComparerTests
{
    public sealed class AssetComparerTests
    {
        [Test]
        public void OnNewData_OnNewAssetsFound_NewAssetAdded()
        {
            // Arrange
            int numNewAssets = 0;

            ComputerAsset asset1 = new ComputerAsset("Model1", "SerialNumber1", "1");
            ComputerAsset asset2 = new ComputerAsset("Model1", "SerialNumber1", "2");

            List<Asset> currentAssets = new List<Asset> {asset1};
            List<Asset> assetsFromList = new List<Asset> {asset1, asset2};
            
            AssetComparer<Asset> assetComparer = new AssetComparer<Asset>(currentAssets);
            
            // Act
            assetComparer.NewAssetsFound += newAssets =>
            {
                numNewAssets = newAssets.Count;
            };

            assetComparer.OnNewData(assetsFromList);
            
            // Assert
            Assert.That(numNewAssets, Is.EqualTo(1));
        }
        
        [Test]
        public void OnNewData_AssetMissingInList_AssetIsOffline()
        {
            // Arrange
            new AssetRecordManager().StartWatchingForAssetStatusChange();
            
            Asset asset1 = new ComputerAsset("Model1", "SerialNumber1", "1");
            Asset asset2 = new ComputerAsset("Model2", "SerialNumber2", "2");
            
            List<Asset> currentAssets = new List<Asset> {asset1, asset2};
            List<Asset> assetsFromList = new List<Asset> {asset1};
            
            AssetComparer<Asset> assetComparer = new AssetComparer<Asset>(currentAssets);
            
            // Act
            assetComparer.OnNewData(assetsFromList);
            
            // Assert
            Assert.That(asset2.LastAssetRecord.State, Is.EqualTo(AssetState.Missing));
        }
        
        [Test]
        public void OnNewData_AssetReappearsInList_AssetIsOnline()
        {
            // Arrange
            new AssetRecordManager().StartWatchingForAssetStatusChange();
            
            Asset asset1 = new ComputerAsset("Model1", "SerialNumber1", "1");
            Asset asset2 = new ComputerAsset("Model2", "SerialNumber2", "2");

            asset1.SetState(AssetState.Online);
            asset2.SetState(AssetState.Missing);
            
            List<Asset> currentAssets = new List<Asset> {asset1, asset2};
            List<Asset> assetsFromList = new List<Asset> {asset1, asset2};
            
            AssetComparer<Asset> assetComparer = new AssetComparer<Asset>(currentAssets);
            
            // Act
            assetComparer.OnNewData(assetsFromList);
            
            // Assert
            Assert.That(asset2.LastAssetRecord.State, Is.EqualTo(AssetState.Online));
        }
        
        [Test]
        public void OnNewData_NewAssetInList_AssetIsOnline()
        {
            // Arrange
            new AssetRecordManager().StartWatchingForAssetStatusChange();
            
            Asset asset1 = new ComputerAsset("Model1", "SerialNumber1", "1");
            Asset asset2 = new ComputerAsset("Model2", "SerialNumber2", "2");
            
            List<Asset> currentAssets = new List<Asset> {asset1};
            List<Asset> assetsFromList = new List<Asset> {asset1, asset2};
            
            AssetComparer<Asset> assetComparer = new AssetComparer<Asset>(currentAssets);
            
            // Act
            assetComparer.OnNewData(assetsFromList);
            
            // Assert
            Assert.That(asset2.LastAssetRecord.State, Is.EqualTo(AssetState.Online));
        }
    }
}