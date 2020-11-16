using AssetManagement.Core;
using AssetManagement.Models.Asset;
using AssetManagement.Models.AssetHolder;
using NSubstitute;
using NUnit.Framework;

namespace AssetManagement.NUnitTests
{
    [TestFixture]
    public sealed class MoveAssetTests
    {
        [Test]
        public void MoveAssetToDepot_MoveAsset_SuccessfullyMoveAsset()
        {
            // Arrange
            Asset asset = new Asset(1, "M1", "Apple");
            
            // Act
            asset.Transfer.ToDepot();
            
            // Assert
            Assert.AreEqual(asset.CurrentAssetHolder, StaticAssetHolders.Depot,
                $"Expected {asset.CurrentAssetHolder}. But got {StaticAssetHolders.Depot.Label}"
            );
        }

        [Test]
        public void MoveAssetToCage_MoveAsset_SuccessfullyMoveAsset()
        {
            // Arrange
            Asset asset = new Asset(1, "M1", "Apple");

            // Act
            asset.Transfer.ToCage();

            // Assert
            Assert.AreEqual(asset.CurrentAssetHolder, StaticAssetHolders.Cage, 
                $"Expected {asset.CurrentAssetHolder}. But got {StaticAssetHolders.Cage.Label}");
        }
    }
}
