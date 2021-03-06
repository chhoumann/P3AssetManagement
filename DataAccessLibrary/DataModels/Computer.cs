using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels.Handlers;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AssetManagement.DataAccessLibrary.DataModels
{
    public class Computer : IAsset
    {
        public Computer(string pcName, string operatingSystem, string manufacturer,
            List<string> models, string serialNumber) : this()
        {
            PcName = pcName;
            OperatingSystem = operatingSystem;
            Manufacturer = manufacturer;
            SerialNumber = serialNumber;

            models?.ForEach(model => Models.Add(new ComputerModel(model)));
        }

        public Computer(string serialNumber) : this() => SerialNumber = serialNumber;

        public string AssetId => PcName;

        public void AddAssetRecord(AssetHolder holder, DateTime timestamp, AssetState state)
        {
            ComputerRecords.Add(new ComputerRecord(this, holder, timestamp, state));
        }

        public IReadOnlyList<IAssetRecord> AssetRecords => ComputerRecords.Cast<IAssetRecord>().ToList();
        public IAssetRecord LastAssetRecord => ComputerRecords.OrderByDescending(x => x.Timestamp).First();
        public AssetHolder CurrentHolder => LastAssetRecord.Holder;
        public DateTime LastChanged => LastAssetRecord.Timestamp;
        public AssetState CurrentState => LastAssetRecord.State;

        public IAssetOwnershipHandler Transfer => new AssetOwnershipHandler<Computer, ComputerService>(this);

        public AssetStateHandler ChangeState => new AssetStateHandler(this);

        #region EF Core Stuff
        public Computer()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; private set; }

        [Required]
        [MaxLength(50)]
        public string PcName { get; set; }

        [MaxLength(50)]
        public string OperatingSystem { get; private set; }

        [MaxLength(50)]
        public string Manufacturer { get; private set; }

        [Required]
        [MaxLength(50)]
        public string SerialNumber { get; private set; }

        [MaxLength(30)]
        public string PcAdStatus { get; set; }

        public List<ComputerModel> Models { get; private set; } = new List<ComputerModel>();
        public List<ComputerRecord> ComputerRecords { get; private set; } = new List<ComputerRecord>();
        #endregion
    }
}
