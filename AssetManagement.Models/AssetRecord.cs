using System;

namespace AssetManagement.Models
{
    public enum AssetState { Missing, Online }

    public class AssetRecord
    {
        public AssetHolder CurrentHolder { get; }
        public DateTime Date { get; }
        public AssetState State { get; }

        public AssetRecord(AssetState state, AssetHolder currentHolder)
        {
            State = state;
            CurrentHolder = currentHolder;
            Date = DateTime.Now;
        }
    }
}
