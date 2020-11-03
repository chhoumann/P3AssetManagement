using AssetManagement.Core;
using AssetManagement.DataReader;
using AssetManagement.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace AssetManagement.NUnitTests
{
    class ComputerDataLineParserTests
    {
        public int ExpectedAmountOfFieldsInCsvLine { get; private set; }
        public char Sepparator { get; private set; }
        public string FileName { get; private set; }

        [SetUp]
        public void Setup()
        {
            ExpectedAmountOfFieldsInCsvLine = 11;
            Sepparator = ';';
            FileName = "FileName.csv";
        }

        // Actual examples from dataset
        [TestCase("02/10/20 13:36;N1M001;Nikhil Copeland;\"Løn, HR og Arbejdsmiljø\";PC5001139;Windows 10;Dell Inc.;OptiPlex 7010;01;6CRPJ5J;Aktiv")]
        [TestCase("30/09/20 14:23;N1M003;Jazlene Higgins;MP Vandløb;PC5001299;Windows 10;Dell Inc.;Latitude E7250;;1DR7F72;Aktiv")]
        [TestCase("05/09/20 09:45;N1M456;Uriel Hill;AFE Projekter;PC5004918;Windows 10;American Megatrends Inc.;Surface Pro 3;1;033880650753;Deaktiveret")]
        [TestCase("01/09/20 07:59;#I/T;#I/T;\"AFS Økonomi, Finans\";PC5001544;Windows 10;Dell Inc.;Latitude E7250;;8B9N462;Deaktiveret")]
        public void ComputerDataLineParse_Parsing_ParseSuccess(string csvRow)
        {
            // Arrange
            ComputerDataLineParser computerDataLineParser = new ComputerDataLineParser();
            CsvLineParser parseFunc = computerDataLineParser.GetParseFunc();
            string[] fields = csvRow.Split(Sepparator);
            // Act
            TestDelegate parseData = () => parseFunc(FileName, fields);
            // Assert
            Assert.DoesNotThrow(parseData);
        }

        [TestCase("HEJ 13:36")]
        [TestCase("02/02/20 HEJ")]
        [TestCase("02/1/0/20 13:36")]
        [TestCase("02/30/20 13:36")]
        [TestCase("#I/T")]
        [TestCase("A/02/20 13:36")]
        [TestCase("02/A/20 13:36")]
        [TestCase("02/02/A 13:36")]
        [TestCase("02/02/20 A:36")]
        [TestCase("02/02/20 13:A")]
        public void ComputerDataLineParse_ParsingCorruptDate_ThrowFormatException(string corruptDate)
        {
            // Arrange
            // Append the correct amount of mock data to the corruptdate
            string mockData = string.Concat(Enumerable.Repeat(";foo", ExpectedAmountOfFieldsInCsvLine - 1));
            string csvRowWithCorruptDate = corruptDate + mockData;
            string[] fieldsWithCorruptDate = csvRowWithCorruptDate.Split(Sepparator);
            ComputerDataLineParser computerDataLineParser = new ComputerDataLineParser();
            CsvLineParser parseFunc = computerDataLineParser.GetParseFunc();
            // Act
            TestDelegate parseData = () => parseFunc(FileName, fieldsWithCorruptDate);
            // Assert
            Assert.Throws<FormatException>(parseData);
        }
    }
}
