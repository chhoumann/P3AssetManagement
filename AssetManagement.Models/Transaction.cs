using System;

namespace AssetManagement.Models
{
    public class Transaction
    {
        public AssetHolder Giver { get; }
        public AssetHolder Receiver { get; }
        public IAsset Asset { get; }
        public DateTime Date { get; }

        public Transaction(AssetHolder giver, AssetHolder receiver, IAsset asset)
        {
            Giver = giver;
            Receiver = receiver;
            Asset = asset;
            Date = DateTime.Now;
        }
    }
}
