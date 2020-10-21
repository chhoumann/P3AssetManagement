using System;
using System.Collections.Generic;
using System.IO;

namespace DataReader
{
    class CSVParser
    {
        public List<AAFData> LoadAAFDataFromCSV(string path)
        {
            List<AAFData> data = new List<AAFData>();
            StreamReader sr;
            try
            {
                sr = new StreamReader(path);
            }
            catch
            {

                throw new FileNotFoundException("File not found on given path");
            }
            
            
            // First line is skipped, as it includes headers and has no actual data.
            string headerLine = sr.ReadLine();

            // Reads all lines in csv, and saves them in an AAFData list.
            string currentLine;
            while ((currentLine = sr.ReadLine()) != null)
            {
                data.Add(AAFDataFromCSVString(currentLine));
            }
            
            return data;
        }

        private AAFData AAFDataFromCSVString(string csvLine)
        {
            string[] csvFields = csvLine.Split(';');
            try
            {
                // Parses the string-array to an AAFData object
                AAFData parsedData = new AAFData(DateTime.Parse(csvFields[0]),
                    csvFields[1],
                    csvFields[2],
                    csvFields[3],
                    csvFields[4],
                    csvFields[5],
                    Int32.Parse(csvFields[6]),
                    Int32.Parse(csvFields[7]),
                    Int32.Parse(csvFields[8]),
                    csvFields[9],
                    csvFields[10],
                    DateTime.Parse(csvFields[11]),
                    csvFields[12],
                    csvFields[13],
                    csvFields[14]
                    );

                return parsedData;
            }
            catch 
            {
                throw new FormatException("Field in data was not formatted correctly, and could not be parsed");
            }

        }
    }
}
