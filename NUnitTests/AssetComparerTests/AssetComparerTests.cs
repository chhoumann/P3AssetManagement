using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using AssetManagement.Models;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace AssetManagement.NUnitTests.AssetComparerTests
{
    public sealed class AssetComparerTests
    {
        public Computer Asset1 { get; set; }
        public Computer Asset2 { get; set; }

        [SetUp]
        public void Setup()
        {
            AssetHolder ah = Substitute.For<AssetHolder>();
            ComputerRecord rec = Substitute.For<ComputerRecord>();
            rec.Holder = ah;

            Asset1 = Substitute.For<Computer>();
            Asset2 = Substitute.For<Computer>();

            Asset1.ComputerRecords.Add(rec);
            Asset2.ComputerRecords.Add(rec);
            Asset1.PcName = "pc1";
            Asset2.PcName = "pc2";
        }

        [Test]
        public void OnNewData_NewAssetsFound_NewAssetAdded()
        {
            // Arrange
            List<IAsset> currentAssets = new List<IAsset> { Asset1 };
            List<IAsset> assetsFromList = new List<IAsset> { Asset1, Asset2 };

            AssetComparer<IAsset> assetComparer = new AssetComparer<IAsset>(currentAssets);

            int numNewAssets = 0;
            assetComparer.NewAssetsFound += (newAssets) => { numNewAssets = newAssets.Count; };

            // Act
            assetComparer.OnNewData(assetsFromList);

            // Assert
            Assert.That(numNewAssets, Is.EqualTo(1));
        }

        [Test]
        public void OnNewData_AssetMissingInList_AssetStateIsMissing()
        {
            // Arrange
            List<Computer> currentAssets = new List<Computer> { Asset1, Asset2 };
            List<Computer> assetsFromList = new List<Computer> { Asset1 };

            AssetComparer<Computer> assetComparer = new AssetComparer<Computer>(currentAssets);

            // Act
            assetComparer.OnNewData(assetsFromList);

            // Assert
            Assert.AreEqual(Asset2.CurrentState, AssetState.Missing);
        }

        [Test]
        public void OnNewData_AssetReappearsInList_AssetStateIsOnline()
        {
            // Arrange
            Asset1.ChangeState.ToOnline();
            Asset2.ChangeState.ToMissing();

            List<Computer> currentAssets = new List<Computer> { Asset1, Asset2 };
            List<Computer> assetsFromList = new List<Computer> { Asset1, Asset2 };

            AssetComparer<Computer> assetComparer = new AssetComparer<Computer>(currentAssets);

            // Act
            assetComparer.OnNewData(assetsFromList);

            // Assert
            Assert.AreEqual(Asset2.CurrentState, AssetState.Online);
        }

        [Test]
        public void OnNewData_NewAssetInList_AssetStateIsOnline()
        {
            // Arrange
            List<Computer> currentAssets = new List<Computer> { Asset1 };
            List<Computer> assetsFromList = new List<Computer> { Asset1, Asset2 };

            AssetComparer<Computer> assetComparer = new AssetComparer<Computer>(currentAssets);

            // Act
            assetComparer.OnNewData(assetsFromList);

            // Assert
            Assert.AreEqual(Asset2.CurrentState, AssetState.Online);
        }
    }
}