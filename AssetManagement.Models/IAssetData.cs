using System;
using System.Collections.Generic;

namespace AssetManagement.Models
{
    public interface IAssetData 
    {
        string Model { get; }
        string SerialNumber { get; }
        int Id { get; }

        DateTime LastChanged { get; }

        AssetHolder CurrentAssetHolder { get; }
        AssetRecord LastAssetRecord { get; }
        List<AssetRecord> AssetRecords { get; }
    }
}