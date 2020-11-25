using AssetManagement.Models.Asset;
using AssetManagement.Models.AssetHolder;
using NUnit.Framework;

namespace AssetManagement.NUnitTests
{
    [TestFixture]
    public sealed class MoveAssetTests
    {
        [Test]
        public void MoveComputerToDepot_MoveAsset_SuccessfullyMoveAsset()
        {
            // Arrange
            Asset asset = new ComputerAsset("ASSET_ID", "MODEL_NAME", "SERIAL_NUMBER");

            // Act
            asset.Transfer.ToDepot();

            // Assert
            Assert.AreEqual(StaticAssetHolders.Depot, asset.CurrentAssetHolder);
        }

        [Test]
        public void MoveComputerToCage_MoveAsset_SuccessfullyMoveAsset()
        {
            // Arrange
            Asset asset = new ComputerAsset("ASSET_ID", "MODEL_NAME", "SERIAL_NUMBER");

            // Act
            asset.Transfer.ToCage();

            // Assert
            Assert.AreEqual(StaticAssetHolders.Cage, asset.CurrentAssetHolder);
        }
    }
}
