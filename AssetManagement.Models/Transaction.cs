using System;

namespace AssetManagement.Models
{
    public sealed class Transaction
    {
        public AssetHolder Giver { get; }
        public AssetHolder Receiver { get; }
        public DateTime Date { get; }

        public Transaction(AssetHolder giver, AssetHolder receiver)
        {
            Giver = giver;
            Receiver = receiver;
            Date = DateTime.Now;
        }
    }
}
