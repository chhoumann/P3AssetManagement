using AssetManagement.Core;
using System;

namespace AssetManagement.Models
{
    public class ComputerData : IAssetRecord
    {
        #region IAssetRecord properties
        public string FileName { get; }
        public string AssetId { get; }
        public DateTime Date { get; }
        public IAssetHolder Holder { get; }
        public AssetState State { get; }
        #endregion

        #region Other properties
        public string OperatingSystem { get; }
        public string Manufacturer { get; }
        public string Model1 { get; }
        public string Model2 { get; }
        public string SerialNumber { get; }
        public string PcadStatus { get; }
        #endregion

        public ComputerData(string originFileName, DateTime timestamp, string username, string name, string department, string pcName, string operatingSystem,
                       string manufacturer, string model1, string model2, string serialNumber, string pcadStatus)
        {
            // IAssetRecord
            FileName = originFileName;
            Date = timestamp;
            Holder = new AssetHolder(name, username, department);
            State = AssetState.Online;
            // Other
            AssetId = pcName;
            OperatingSystem = operatingSystem;
            Manufacturer = manufacturer;
            Model1 = model1;
            Model2 = model2;
            SerialNumber = serialNumber;
            PcadStatus = pcadStatus;
        }

    }
}
