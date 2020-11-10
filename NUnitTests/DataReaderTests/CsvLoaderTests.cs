using AssetManagement.Core;
using AssetManagement.DataReader;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace AssetManagement.NUnitTests.DataReaderTests
{
    public class CsvLoaderTests
    {
        public string TestFilesDir { get; private set; }
        public int ExpectedFieldsAmount { get; private set; }
        public char Sepparator { get; private set; }
        public int ActualLinesInCsvExcludingHeaderAndNewLineAtEndOfFile { get; private set; }
        public CsvLineParser SkipParser { get; private set; }

        [SetUp]
        public void Setup()
        {
            ExpectedFieldsAmount = 11;
            Sepparator = ';';
            ActualLinesInCsvExcludingHeaderAndNewLineAtEndOfFile = 872;

            // TestDirectory is this dir: p3\NUnitTests\bin\Debug\netcoreapp3.1
            TestFilesDir = Path.Combine(TestContext.CurrentContext.TestDirectory, "test-files");
            SkipParser = (string originFileName, string[] fields) =>
            {
                // This is where the parsing should've happened
                IAssetRecord assetRecord = Substitute.For<IAssetRecord>();

                return assetRecord;
            };
        }
        [Test]
        public void LoadDataFromCsv_LoadingData_LoadDataWithoutThrowing()
        {
            // Arrange
            CsvLoader loader = new CsvLoader();
            string fileDir = Path.Combine(TestFilesDir, "ComputerDataExample_CorrectDataSet.csv");
            // Act
            TestDelegate loadData = () => loader.LoadDataFromCsv(fileDir, ExpectedFieldsAmount, Sepparator, SkipParser);
            // Assert
            Assert.DoesNotThrow(loadData);
        }

        [Test]
        public void LoadDataFromCsv_LoadingData_LoadAllLinesFromFile()
        {
            // Arrange
            CsvLoader loader = new CsvLoader();
            string fileDir = Path.Combine(TestFilesDir, "ComputerDataExample_CorrectDataSet.csv");
            // Act
            List<IAssetRecord> parsedData = loader.LoadDataFromCsv(fileDir, ExpectedFieldsAmount, Sepparator, SkipParser);
            // Assert
            Assert.AreEqual(ActualLinesInCsvExcludingHeaderAndNewLineAtEndOfFile, parsedData.Count);
        }

        [TestCase("ComputerDataExample_OneFieldTooMany.csv")]
        [TestCase("ComputerDataExample_OneFieldTooFew.csv")]
        [TestCase("ComputerDataExample_Empty.csv")]
        public void LoadDataFromCsv_LoadingDataWithIncorrectNumberOfFields_ThrowArgumentException(string fileName)
        {
            // Arrange
            CsvLoader loader = new CsvLoader();
            string fileDir = Path.Combine(TestFilesDir, fileName);
            // Act
            TestDelegate loadData = () => loader.LoadDataFromCsv(fileDir, ExpectedFieldsAmount, Sepparator, SkipParser);
            // Assert
            Assert.Throws<ArgumentException>(loadData);
        }

        [Test]
        public void LoadDataFromCsv_FileNotExisting_ThrowFileNotFoundException()
        {
            // Arrange
            CsvLoader loader = new CsvLoader();
            string fileDir = Path.Combine(TestFilesDir, "ThisFileDoesNotExist.csv");
            // Act
            TestDelegate loadData = () => loader.LoadDataFromCsv(fileDir, ExpectedFieldsAmount, Sepparator, SkipParser);
            // Assert
            Assert.Throws<FileNotFoundException>(loadData);
        }
    }
}