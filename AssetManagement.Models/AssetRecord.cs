using System;

namespace AssetManagement.Models
{
    public enum AssetState { Missing, Online }

    public class AssetRecord
    {
        public AssetHolder Holder { get; }
        public DateTime Date { get; }
        public AssetState State { get; }

        public AssetRecord(AssetState state, AssetHolder holder)
        {
            State = state;
            Holder = holder;
            Date = DateTime.Now;
        }
    }
}
