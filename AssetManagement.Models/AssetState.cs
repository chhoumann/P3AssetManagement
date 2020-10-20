using System;

namespace AssetManagement.Models
{
    public class AssetState
    {
        public State State { get; private set; }
        public DateTime Date { get; private set; }

        public AssetState(State state)
        {
            State = state;
            Date = DateTime.Now;
        }
    }
}
