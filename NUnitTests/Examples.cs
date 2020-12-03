using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.Generic;
using AssetManagement.Models.Asset;
using AssetManagement.Models.AssetHolder;
using AssetManagement.Models.AssetRecord;
using NSubstitute;
using NUnit.Framework;

/* Method Naming Conventions. Should be separated by '_':
 * - The name of the method being tested.
 * - The scenario under which it's being tested.
 * - The expected behavior when the scenario is invoked.
 */
namespace AssetManagement.NUnitTests
{
    public sealed class Examples
    {
        // Allows you to run functions before the test cases are run.
        [SetUp]
        public void Setup()
        {
        }

        // [TestCase()] // TestCases can be used to test similar behavior without writing many repetitive tests.
        [Test]
        public void ComputerTransferTo_AssetTransferOfAssetWithoutHolder_OwnershipTransferredSuccessfully()
        {
            // Arrange
            IAssetHolder assetHolder = Substitute.For<IAssetHolder>();
            Asset asset = new ComputerAsset(null, "ASSET_ID", "MODEL_NAME", "SERIAL_NUMBER");
            new AssetRecordManager(Substitute.For<ISqlDataAccess<AssetRecordData>>()).StartWatchingForAssetStatusChange();

            // Act
            asset.Transfer.ToUser(assetHolder);

            // Assert
            Assert.AreEqual(assetHolder, asset.CurrentAssetHolder);
        }
    }
}