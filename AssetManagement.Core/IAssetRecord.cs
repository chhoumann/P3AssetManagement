using System;

namespace AssetManagement.Core
{
    public interface IAssetRecord
    {
        DateTime Date { get; }
        IAssetHolder Holder { get; }
        AssetState State { get; }
    }
}