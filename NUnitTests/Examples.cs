using System;
using System.Threading;
using AssetManagement.Models;
using NUnit.Framework;

namespace NUnitTests
{
    public class Examples
    {
        // Allows you to run functions before the test cases are run.
        [SetUp]
        public void Setup()
        {
        }

        // TestCases can be used to test similar behavior without writing many repetitive tests.
        [TestCase(12345, "Epic Dell Gaming PC",     "SN1BFE4", "Ulf",    "ulf@acme.dk")]
        
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

        // Testing if transfering ownership to the currentholder throws an exception
        [Test]
        public void TransferOwnership_WhenTransferingAssetToTheHolderWhoAlreadyHasIt_ShouldThrowException()
        {
            // Arrange
            IAsset asset = AssetController.MakeAsset(1234, "Testing computer", "abc123");
            Employee employee = new Employee("Bjarne", "Bjarne@testingCompany.com");
            AssetController.TransferOwnership(asset, employee);
            
            // Act
            
            // Assert
            Assert.Throws<ArgumentException>(() => AssetController.TransferOwnership(asset, employee));
        }
    }
}