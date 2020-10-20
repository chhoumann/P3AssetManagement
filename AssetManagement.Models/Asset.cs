using System;
using System.Collections.Generic;

namespace AssetManagement.Models
{
    public enum AssetState { Missing, Recovered }
 
    public class Asset
    {
        public string Model { get; private set; }
        public string SerialNumber { get; private set; }

        public int Id { get; private set; }
        
        public DateTime LastChanged { get; private set; }
        
        public AssetHolder CurrentAssetHolder { get; private set; }
        public List<Transaction> Transactions = new List<Transaction>();

        public StateRecord State { get; private set; }

        public Asset(int id, string name, string serialNumber, AssetHolder currentAssetHolder)
        {
            Model = name;
            SerialNumber = serialNumber;
            Id = id;
            CurrentAssetHolder = currentAssetHolder;
            
            State = new StateRecord(AssetState.Recovered);
            
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

        public class StateRecord
        {
            public AssetState State { get; private set; }
            public DateTime Date { get; private set; }

            public StateRecord(AssetState state)
            {
                State = state;
                Date = DateTime.Now;
            }
        }
    }
}
