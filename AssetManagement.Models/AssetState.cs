using System;

namespace AssetManagement.Models
{
    public enum State { Missing, Recovered }

    public class AssetState
    {
        public State State { get; set; }
        public DateTime Date { get; set; }

        public AssetState(State state)
        {
            State = state;
            Date = DateTime.Now;
        }

    }
}