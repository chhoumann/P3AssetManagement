using System;

namespace AssetManagement.Models.LoadDataFromFiles.ComputerData
{
    public class ComputerData
    {
        public string FileName { get; }
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

        public ComputerData(string fileName, string csvLine)
        {
            string[] fields = csvLine.Split(';');
            FileName = fileName;
            Timestamp = Convert.ToDateTime(fields[0]);
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

        public override string ToString()
        {
            return $"{Timestamp}: Holder: {Holder}, PcName: {PcName}";
        }
    }
}
