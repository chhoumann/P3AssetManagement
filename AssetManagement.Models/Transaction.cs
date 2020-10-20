﻿using System;

namespace AssetManagement.Models
{
    public class Transaction
    {
        public AssetHolder Giver { get; private set; }
        public AssetHolder Receiver { get; private set; }
        public Asset Asset { get; private set; }
        public DateTime Date { get; private set; }

        public Transaction(AssetHolder giver, AssetHolder receiver, Asset asset)
        {
            Giver = giver;
            Receiver = receiver;
            Asset = asset;
            Date = DateTime.Now;
        }
    }
}