using System;
using System.Collections.Generic;

namespace AssetManagement.Models
{
    public interface IAsset
    {
        string Model { get; }
        string SerialNumber { get; }
        int Id { get; }

        DateTime LastChanged { get; }

        AssetHolder CurrentAssetHolder { get; }
        StateRecord State { get; }
        
        List<Transaction> Transactions { get; }
    }
}