using AssetManagement.DataAccessLibrary.DataModels;
using NSubstitute;
using NUnit.Framework;

// TODO: Should do something about this
namespace AssetManagement.NUnitTests
{
    [TestFixture]
    public sealed class MoveAssetTests
    {
        [Test]
        public void MoveComputerToDepot_MoveAsset_SuccessfullyMoveAsset()
        {
            // Arrange
            Computer computer = Substitute.For<Computer>();

            //    // Act
            //    asset.TransferTo.ToDepot();

            //    // Assert
            //    Assert.AreEqual(StaticAssetHolders.Depot, asset.CurrentAssetHolder);
            //}
        }

        [Test]
        public void MoveComputerToCage_MoveAsset_SuccessfullyMoveAsset()
        {
            // Arrange
            Computer computer = Substitute.For<Computer>();

            //    // Act
            //    asset.TransferTo.ToCage();

            //    // Assert
            //    Assert.AreEqual(StaticAssetHolders.Cage, asset.CurrentAssetHolder);
            //}
        }
    }
}