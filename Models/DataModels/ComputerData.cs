using System;
using System.Globalization;

namespace AssetManagement.Models.DataModels
{
    public class ComputerData
    {
        public string FilePath { get; }
        public string PcName { get; }
        public DateTime Timestamp { get; }
        public string Holder { get; }
        public string Username { get; }
        public string Department { get; }
        public string OperatingSystem { get; }
        public string Manufacturer { get; }
        public string Model1 { get; }
        public string Model2 { get; }
        public string SerialNumber { get; }
        public string PcadStatus { get; }

        /// <summary>
        /// This constructor can construct this object from a line of csv data,
        /// which is provided by PC-ID.
        /// </summary>
        /// <param name="filePath">The file path to where the csvLine originates from.</param>
        /// <param name="separator">The char which separates the fields in the csvLine. This is usually ',' or ';'.</param>
        /// <param name="cultureInfo">Tells the method how to parse the Timestamp</param>
        /// <param name="csvLine">A string of fields separated by the separator parameter.</param>
        public ComputerData(string filePath, char separator, CultureInfo cultureInfo, string csvLine)
        {
            string[] fields = csvLine.Split(separator);
            FilePath = filePath;
            Timestamp = Convert.ToDateTime(fields[0], cultureInfo);
            Username = fields[1];
            Holder = fields[2];
            Department = fields[3];
            PcName = fields[4];
            OperatingSystem = fields[5];
            Manufacturer = fields[6];
            Model1 = fields[7];
            Model2 = fields[8];
            SerialNumber = fields[9];
            PcadStatus = fields[10];
        }

        public override string ToString() => $"{Timestamp}: Holder: {Holder}, PcName: {PcName}";
    }
}
