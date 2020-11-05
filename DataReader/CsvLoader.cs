using AssetManagement.Core;
using System;
using System.Collections.Generic;
using System.IO;


namespace AssetManagement.DataReader
{
    public delegate IAssetRecord CsvLineParser(string fileName, string[] fields);


    public sealed class CsvLoader
    {
        /// <summary>
        /// Creates a list of AAFData structs from a semicolon seperated AAF csv-file. 
        /// </summary>
        /// <param name="path">The path to the csv file.</param>
        /// <param name="sepparator">The sepparator used in the csv file</param>
        /// <param name="parseFunc">A callback function which parses a row from the data to a generic object</param>
        /// <param name="expectedFieldsAmount">The amount of fields which is expected to be present in each line of the csv file</param>
        /// <returns>A list of data of the type specified by the parseFunc</returns>
        public List<IAssetRecord> LoadDataFromCsv(string path, int expectedFieldsAmount, char sepparator, CsvLineParser parseFunc)
        {
            List<IAssetRecord> data = new List<IAssetRecord>();
            StreamReader sr = new StreamReader(path);
            string fileName = Path.GetFileName(path);
            //lineCount only for error
            int lineCount = 1;

            // First line is skipped, as it includes headers and has no actual data.
            string currentLine = sr.ReadLine();
            if (string.IsNullOrEmpty(currentLine))
            {
                throw new ArgumentException($"The file at >{path}< was empty.");
            }
            lineCount++;

            // Reads all lines in csv, and saves them in an AAFData list.
            do
            {
                currentLine = sr.ReadLine();
                if (currentLine != null)
                {
                    string[] fields = currentLine.Split(sepparator);

                    if (fields.Length != expectedFieldsAmount)
                    {
                        throw new ArgumentException($"The fields in line {lineCount} ({fields.Length}) " +
                                                    $"and the expectedFieldsAmount ({expectedFieldsAmount}) does not correspond. " +
                                                    $"Parsing failed.");
                    }

                    data.Add(parseFunc(fileName, fields));
                    lineCount++;
                }
            } while (currentLine != null);

            return data;
        }
    }
}
