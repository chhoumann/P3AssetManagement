using AssetManagement.Models;
using NUnit.Framework;

namespace NUnitTests
{
    public class Examples
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(12345, "Epic Dell Gaming PC",     "SN1BFE4", "Ulf",    "ulf@acme.dk")]
        [TestCase(12346, "Unepic Dell Gaming PC",   "SN1BFE3", "Rolf",   "rolf@acme.dk")]
        [TestCase(12347, "HP Officejet Pro 6230 e", "SN1BFE5", "Sofus",  "sofus@acme.dk")]
        [TestCase(12348, "Acer Awesome",            "SN1BFE6", "Bertil", "bertil@acme.dk")]
        [TestCase(12349, "Toshiba Tanker",          "SN1BFE7", "Mads",   "mads@acme.dk")]
        public void When_NewAsset_Expect_AssetCanBeTransferedToEmployee(int assetId, string assetName, string assetSerialNumber, 
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
                asset.CurrentAssetHolder.Name, employee.Name);
        }
    }
}