using System;

namespace AssetManagement.Models
{
    public class Transaction
    {
        public AssetHolder Giver { get; set; }
        public AssetHolder Receiver { get; set; }
        public Asset Asset { get; set; }
        public DateTime Date { get; set; }

        public Transaction(AssetHolder giver, AssetHolder receiver, Asset asset)
        {
            Giver = giver;
            Receiver = receiver;
            Asset = asset;
            Date = DateTime.Now;
        }
    }
}