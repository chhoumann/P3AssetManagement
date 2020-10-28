using AssetManagement.Models;
using NUnit.Framework;

/* Method Naming Conventions. Should be sepparated by '_':
 * - The name of the method being tested.
 * - The scenario under which it's being tested.
 * - The expected behavior when the scenario is invoked.
 */
namespace AssetManagement.NUnitTests

{
    public class Examples
    {
        // Allows you to run functions before the test cases are run.
        [SetUp]
        public void Setup()
        {
        }

        // TestCases can be used to test similar behavior without writing many repetitive tests.
        [TestCase(12345, "Epic Dell Gaming PC", "SN1BFE4", "Ulf", "ulf@acme.dk")]
        public void TransferOwnership_AssetTransfer_AssetOwnershipTransfered(int assetId, string assetName, string assetSerialNumber,
            string employeeName, string employeeEmail)
        {
            // Arrange
            IAsset asset = AssetController.MakeAsset(assetId, assetName, assetSerialNumber);
            Employee employee = new Employee(employeeName, employeeEmail);
            // Act
            AssetController.TransferOwnership(asset, employee);
            // Assert
            Assert.AreEqual(asset.CurrentAssetHolder, employee,
                "Expected {0}. But got {1}",
                asset.CurrentAssetHolder, employee.Name);
        }
    }
}