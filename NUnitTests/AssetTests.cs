using AssetManagement.Models;
using NUnit.Framework;

namespace NUnitTests
{
    public class AssetTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
       
        public void TransferTo()
        {
            // Arrange
            Asset dellPC = new Asset(69420, "Epic Dell Gaming PC", "SN1");
            Employee ulf = new Employee("Ulf", "ulf@acme.dk");
            // Act
            dellPC.TransferTo(ulf);
            // Assert
            Assert.AreEqual(dellPC.CurrentAssetHolder, ulf,
                "Expected {0}. But got {1}",
                dellPC.CurrentAssetHolder.Name, ulf.Name);

            // Arrange
            Employee hanne = new Employee("Hanne", "hanne@acme.dk");
            // Act
            dellPC.TransferTo(hanne);
            // Assert
            Assert.AreEqual(dellPC.CurrentAssetHolder, hanne,
                "Expected {0}. But got {1}",
                dellPC.CurrentAssetHolder.Name, hanne.Name);

            // Arrange
            Depot depot = new Depot();
            // Act
            dellPC.TransferTo(depot);
            // Assert
            Assert.AreEqual(dellPC.CurrentAssetHolder, depot,
                "Expected {0}. But got {1}",
                dellPC.CurrentAssetHolder.Name, depot.Name);
        }

        [TestCase(1, 2, 3, Description = "Testing if 1+2 is 3")]
        [TestCase(1, 3, 3, Description = "Testing if 1+3 is 3")]
        public void SumTest(int a, int b, int result)
        {
            Asset summingAsset = new Asset(66, "TestingAsset", "1234");
            int methodReturnValue = summingAsset.Sum(a, b);
            Assert.AreEqual(result, methodReturnValue);
        }
    }

   
}