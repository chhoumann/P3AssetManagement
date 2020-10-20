using System;

namespace AssetManagement.Models
{
    public enum AssetState { Missing, Recovered }

    public class StateRecord
    {
        public AssetState State { get; }
        public DateTime Date { get; }

        public StateRecord(AssetState state)
        {
            State = state;
            Date = DateTime.Now;
        }
    }
}
