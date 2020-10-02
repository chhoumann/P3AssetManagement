using System;

namespace AssetManagement.Models
{
    public sealed class AnalogueAsset : Asset
    {
        public AnalogueAsset(string name) : base(name) => state = AssetState.Offline;

        public override AssetState State 
        { 
            get => base.State; 
            set
            {
                if (value == AssetState.Online)
                {
                    throw new InvalidOperationException("Attempt to set analogue asset state to the online state!");
                }
                else
                {
                    state = value;
                }
            }
        }
    }
}