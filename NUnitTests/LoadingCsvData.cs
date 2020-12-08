﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssetManagement.Core.DataLoadStrategy;
using AssetManagement.DataAccessLibrary.DataModels;
using NUnit.Framework;

namespace AssetManagement.NUnitTests
{
    public sealed class LoadingCsvData
    {
        [Test]
        public void GetComputerDataFromFile_ReadsDataAndReturnsListOfComputerData_DataIsNotNull()
        {
            // Arrange
            string filePath = Path.Combine(Environment.CurrentDirectory, "test-files", "2020-10-07-PCID.csv");
            const char separator = ';';

            // Act
            List<Computer> data = new ComputerDataCsvLoader(separator).ReadData(filePath).ToList();

            // Assert
            Assert.IsNotNull(data);
        }
    }
}