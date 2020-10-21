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

        List<StateRecord> Records { get; }
        List<Transaction> Transactions { get; }
    }
}