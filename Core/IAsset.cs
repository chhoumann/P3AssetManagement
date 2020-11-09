using System;
using System.Collections.Generic;

namespace AssetManagement.Core
{
    public interface IAsset
    {
        string Model { get; }
        string SerialNumber { get; }
        int Id { get; }

        DateTime LastChanged { get; }

        IAssetHolder CurrentAssetHolder { get; }
        IAssetRecord LastAssetRecord { get; }
        List<IAssetRecord> AssetRecords { get; }

        void TransferTo(IAssetHolder receiver);
    }
}