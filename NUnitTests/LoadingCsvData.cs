using AssetManagement.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace AssetManagement.NUnitTests
{
    public class LoadingCsvData
    {

        [Test]
        public void GetComputerDataFromFile_ReadsDataAndReturnsListOfComputerData_DataIsNotNull()
        {
            // Arrange
            string filePath = Path.Combine(Environment.CurrentDirectory, "test-files", "2020-10-07-PCID.csv");
            char sepparator = ';';
            // Act
            List<ComputerData> data = AssetController.GetComputerDataFromFile(filePath, sepparator);
            // Assert
            Assert.IsNotNull(data);
        }
    }
}
