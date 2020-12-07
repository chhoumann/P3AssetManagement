using System.Collections.Generic;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using AssetManagement.Models;
using NSubstitute;
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

            AssetHolder ah = Substitute.For<AssetHolder>();
            ComputerRecord rec = Substitute.For<ComputerRecord>();
            Computer asset1 = Substitute.For<Computer>();
            Computer asset2 = Substitute.For<Computer>();

            rec.Holder = ah;
            asset1.ComputerRecords.Add(rec);
            asset1.PcName = "pc1";
            asset2.ComputerRecords.Add(rec);
            asset2.PcName = "pc2";

            List<IAsset> currentAssets = new List<IAsset> {asset1};
            List<IAsset> assetsFromList = new List<IAsset> {asset1, asset2};

            AssetComparer<IAsset> assetComparer = new AssetComparer<IAsset>(currentAssets);

            // Act
            assetComparer.NewAssetsFound += newAssets => { numNewAssets = newAssets.Count; };

            assetComparer.OnNewData(assetsFromList);

            // Assert
            Assert.That(numNewAssets, Is.EqualTo(1));
        }

        /*[Test]
        public void OnNewData_AssetMissingInList_AssetIsOffline()
        {
            // Arrange
            var ah = Substitute.For<AssetHolder>();
            var rec = Substitute.For<ComputerRecord>();
            Computer asset1 = Substitute.For<Computer>();
            Computer asset2 = Substitute.For<Computer>();

            rec.Holder = ah;
            asset1.ComputerRecords.Add(rec);
            asset1.PcName = "pc1";
            asset2.ComputerRecords.Add(rec);
            asset2.PcName = "pc2";

            List<Computer> currentAssets = new List<Computer> {asset1, asset2};
            List<Computer> assetsFromList = new List<Computer> {asset1};

            AssetComparer<Computer> assetComparer = new AssetComparer<Computer>(currentAssets);

            // Act
            assetComparer.OnNewData(assetsFromList);

            // Assert
            Assert.That(asset2.LastAssetRecord.State, ChangeStateTo.EqualTo(AssetState.Missing));
        }*/

        //[Test]
        //public void OnNewData_AssetReappearsInList_AssetIsOnline()
        //{
        //    // Arrange
        //    new AssetRecordManager().StartWatchingForAssetStatusChange();

        //    Computer asset1 = Substitute.For<Computer>();
        //    Computer asset2 = Substitute.For<Computer>();

        //    asset1.SetState(AssetState.Online);
        //    asset2.SetState(AssetState.Missing);

        //    List<Computer> currentAssets = new List<Computer> {asset1, asset2};
        //    List<Computer> assetsFromList = new List<Computer> {asset1, asset2};

        //    AssetComparer<Computer> assetComparer = new AssetComparer<Computer>(currentAssets);

        //    // Act
        //    assetComparer.OnNewData(assetsFromList);

        //    // Assert
        //    Assert.That(asset2.LastAssetRecord.State, ChangeStateTo.EqualTo(AssetState.Online));
        //}

        //[Test]
        //public void OnNewData_NewAssetInList_AssetIsOnline()
        //{
        //    // Arrange
        //    new AssetRecordManager().StartWatchingForAssetStatusChange();

        //    Computer asset1 = Substitute.For<Computer>();
        //    Computer asset2 = Substitute.For<Computer>();

        //    List<Computer> currentAssets = new List<Computer> {asset1};
        //    List<Computer> assetsFromList = new List<Computer> {asset1, asset2};

        //    AssetComparer<Computer> assetComparer = new AssetComparer<Computer>(currentAssets);

        //    // Act
        //    assetComparer.OnNewData(assetsFromList);

        //    // Assert
        //    Assert.That(asset2.LastAssetRecord.State, ChangeStateTo.EqualTo(AssetState.Online));
        //}
    }
}