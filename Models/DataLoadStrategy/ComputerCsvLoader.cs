using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using AssetManagement.Core;
using AssetManagement.Core.DataLoadStrategy;
using AssetManagement.Core.Exceptions;
using AssetManagement.DataAccessLibrary.DataModels;

namespace AssetManagement.Models.DataLoadStrategy
{
    public class ComputerCsvLoader : CsvLoaderBase<Computer>
    {
        protected override uint MaxFileReadAttempts => 50;
        private const int FileReadTimeoutInterval = 100;

        public ComputerCsvLoader(char separator) => Separator = separator;
        
        public override IEnumerable<Computer> ReadData(string filePath)
        {
            uint numReadAttempts = 0;

            while (numReadAttempts++ < MaxFileReadAttempts && !IsFileReady(filePath))
            {
                Thread.Sleep(FileReadTimeoutInterval);
            }

            if (numReadAttempts >= MaxFileReadAttempts)
            {
                // TODO: The caller of this method should catch this exception
                throw new FileNotReadException($"CSV file could not be read at path {filePath}!");
            }

            return File
                .ReadAllLines(filePath)
                .Skip(1)
                .Select(csvLine =>
                {
                    string[] fields = csvLine.Split(Separator);

                    Computer computer = new Computer(fields[4], fields[5], fields[6],
                        fields[7], fields[9]);
                    AssetHolder assetHolder = new AssetHolder(fields[2], fields[1], fields[3]);
                    
                    computer.ComputerRecords.Add(new ComputerRecord(computer, assetHolder, DateTime.Now, AssetState.Online));
                    
                    return computer;
                });
        }
    }
}