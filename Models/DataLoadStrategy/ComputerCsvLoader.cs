using AssetManagement.Core;
using AssetManagement.Core.DataLoadStrategy;
using AssetManagement.Core.Exceptions;
using AssetManagement.DataAccessLibrary.DataModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace AssetManagement.Models.DataLoadStrategy
{
    public class ComputerCsvLoader : CsvLoaderBase<Computer>
    {
        protected override uint MaxFileReadAttempts => 50;
        private const int FileReadTimeoutInterval = 100;

        public ComputerCsvLoader(char separator) => Separator = separator;

        public override IEnumerable<Computer> ReadData(string filePath)
        {
            WaitForFileToBeReadable(filePath);

            return File
                .ReadAllLines(filePath)
                .Skip(1)
                .Select(CreateComputerFromCsvLine);
        }

        private void WaitForFileToBeReadable(string filePath)
        {
            uint numReadAttempts = 0;

            while (numReadAttempts++ < MaxFileReadAttempts && !IsFileReady(filePath))
            {
                Thread.Sleep(FileReadTimeoutInterval);
            }

            if (numReadAttempts >= MaxFileReadAttempts)
            {
                throw new FileNotReadException($"CSV file could not be read at path {filePath}!");
            }
        }

        private Computer CreateComputerFromCsvLine(string csvLine)
        {
            string[] fields = csvLine.Split(Separator);
            
            DateTime date = DateTime.ParseExact(fields[0], "dd/MM/yy HH:mm", CultureInfo.InvariantCulture);

            Computer computer = new Computer(fields[4], fields[5], fields[6],
                new List<string>() { fields[7], fields[8] }, fields[9]) { PcAdStatus = fields[10]};
            AssetHolder assetHolder = new AssetHolder(fields[2], fields[1], fields[3]);

            computer.ComputerRecords.Add(new ComputerRecord(computer, assetHolder, date, AssetState.Online));

            return computer;
        }
    }
}