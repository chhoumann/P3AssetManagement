using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using AssetManagement.Core;
using AssetManagement.Core.DataLoadStrategy;
using AssetManagement.DataAccessLibrary.DataModels;

namespace AssetManagement.NUnitTests
{
    public sealed class LoadingCsvData
    {
        [Test]
        public void GetComputerDataFromFile_ReadsDataAndReturnsListOfComputerData_DataIsNotNull()
        {
            // Arrange
            string filePath = Path.Combine(Environment.CurrentDirectory, "test-files", "2020-10-07-PCID.csv");
            char separator = ';';
            
            // Act
            AssetLoadCsvContext<ComputerData> assetLoadCsvContext = new AssetLoadCsvContext<ComputerData>(new ComputerDataCsvStrategy(separator));
            List<ComputerData> data = assetLoadCsvContext.LoadData(filePath);
            
            // Assert
            Assert.IsNotNull(data);
        }
    }
}
