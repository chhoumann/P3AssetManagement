using AssetManagement.Core;
using AssetManagement.DataReader;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace AssetManagement.NUnitTests
{
    class CsvParserTests
    {
        public string FilePath { get; private set; }
        public int ExpectedFieldsAmount { get; private set; }
        public char Sepparator { get; private set; }
        public int ActualLinesInCsvExcludingHeaderAndNewLineAtEndOfFile { get; private set; }
        [SetUp]
        public void Setup()
        {
            ExpectedFieldsAmount = 11;
            Sepparator = ';';
            ActualLinesInCsvExcludingHeaderAndNewLineAtEndOfFile = 872;
        }
        [Test] // Uncomment this line to run the test again. The path doesn't work as it is now.
        public void LoadDataFromCsv_LoadingData_LoadDataAndReturnListOfIAssets()
        {
            // Arrange
            CsvLoader loader = new CsvLoader();
            // TestDirectory is this dir: p3\NUnitTests\bin\Debug\netcoreapp3.1
            FilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "test-files", "CompuerDataExample.csv");
            CsvLineParser skipParser = (string originFileName, string[] fields) =>
            {
                // This is where the parsing should've happened
                IAssetRecord assetRecord = Substitute.For<IAssetRecord>();

                return assetRecord;
            };
            // Act
            List<IAssetRecord> parsedData = loader.LoadDataFromCsv(FilePath, ExpectedFieldsAmount, Sepparator, skipParser);
            // Assert
            Assert.AreEqual(ActualLinesInCsvExcludingHeaderAndNewLineAtEndOfFile, parsedData.Count);
        }
    }
}
