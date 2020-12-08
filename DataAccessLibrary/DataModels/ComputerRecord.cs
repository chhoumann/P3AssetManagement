using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagement.DataAccessLibrary.DataModels
{
    public class ComputerRecord : IAssetRecord
    {
        public ComputerRecord(Computer computer, AssetHolder holder, DateTime timestamp, AssetState state)
            : this()
        {
            Computer = computer;
            Holder = holder;
            Timestamp = timestamp;
            State = state;
        }

        public IAsset Asset => Computer;

        public override string ToString() =>
            $"{nameof(Computer.AssetId)}: {Computer.AssetId}, {nameof(Holder)}: {Holder}, {nameof(Timestamp)}: {Timestamp}, {nameof(State)}: {State}";

        #region EF Core Stuff
        public ComputerRecord()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required] public Computer Computer { get; private set; }

        [Required] public DateTime Timestamp { get; private set; }

        [Required] public AssetState State { get; private set; }

        [Required] public AssetHolder Holder { get; set; }
        #endregion
    }
}