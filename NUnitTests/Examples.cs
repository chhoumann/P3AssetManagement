using AssetManagement.Core;
using AssetManagement.Models;
using NSubstitute;
using NUnit.Framework;
/* Method Naming Conventions. Should be sepparated by '_':
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
        public void TransferOwnership_AssetTransferOfAssetWithoutHolder_OwnershipTranferredSuccessfully()
        {
            // Arrange
            IAsset asset = AssetController.MakeAsset(1234, "Cool Model", "SN123");
            IAssetHolder assetHolder = Substitute.For<IAssetHolder>();
            // Act
            AssetController.TransferOwnership(asset, assetHolder);
            // Assert
            Assert.AreEqual(asset.CurrentAssetHolder, assetHolder,
                $"Expected {asset.CurrentAssetHolder}. But got {assetHolder.Username}"
            );
        }
    }
}