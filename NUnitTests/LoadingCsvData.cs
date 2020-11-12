using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using AssetManagement.Core;
using AssetManagement.Models.DataModels;

namespace AssetManagement.NUnitTests
{
    public class LoadingCsvData
    {

        [Test]
        public void GetComputerDataFromFile_ReadsDataAndReturnsListOfComputerData_DataIsNotNull()
        {
            // Arrange
            string filePath = Path.Combine(Environment.CurrentDirectory, "test-files", "2020-10-07-PCID.csv");
            char separator = ';';
            // Act
            List<ComputerData> data = AssetController.GetComputerDataFromFile(filePath, separator);
            // Assert
            Assert.IsNotNull(data);
        }
    }
}
