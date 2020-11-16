using System;
using System.Collections.Generic;
using AssetManagement.Models.AssetHolder;
using AssetManagement.Models.AssetRecord;

namespace AssetManagement.Models.Asset
{
    public interface IAsset
    {
        string Model { get; }
        string SerialNumber { get; }
        string AssetId { get; }
        int Id { get; }
    }
}