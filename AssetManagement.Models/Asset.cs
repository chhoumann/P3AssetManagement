using System;
using System.Collections.Generic;

namespace AssetManagement.Models
{
    public class Asset
    {
        public enum State { Missing, Recovered }
        
        public virtual string Model { get; private set; }
        public int Id { get; private set; }
        public string SerialNumber { get; private set; }
        public DateTime LastChanged { get; private set; }
        public AssetHolder CurrentAssetHolder { get; private set; }
        public List<Transaction> Transactions = new List<Transaction>();

        protected AssetState state;
        public virtual AssetState State 
        { 
            get => state;
            set => state = value;
        }

        public Asset(int id, string name, string serialNumber, AssetHolder currentAssetHolder)
        {
            Model = name;
            Id = id;
            State = new AssetState(Models.State.Recovered);
            CurrentAssetHolder = currentAssetHolder;
            SerialNumber = serialNumber;
            currentAssetHolder.RecieveAsset(this);
        }

        public void TransferTo(AssetHolder newAssetHolder)
        {
            // Remove this asset from its current holder, if any
            if (CurrentAssetHolder != null)
            {
                CurrentAssetHolder.RemoveAsset(this);
            }

            // Update the current holder by transferring this asset to the new AssetHolder
            newAssetHolder.RecieveAsset(this);
            CurrentAssetHolder = newAssetHolder;
        }

        public void Dispose() { }
    }
}
