using AssetManagement.Core;
using AssetManagement.Models.Asset;
using AssetManagement.Models.AssetHolder;
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
        public void AssetTransferTo_AssetTransferOfAssetWithoutHolder_OwnershipTransferredSuccessfully()
        {
            // Arrange
            IAsset asset = new Asset(1234, "Cool Model", "SN123");
            IAssetHolder assetHolder = Substitute.For<IAssetHolder>();
            // Act
            asset.TransferTo(assetHolder);
            // Assert
            Assert.AreEqual(asset.CurrentAssetHolder, assetHolder,
                $"Expected {asset.CurrentAssetHolder}. But got {assetHolder.Username}"
            );
        }
    }
}