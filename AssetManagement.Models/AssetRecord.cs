using System;
using AssetManagement.Core;

namespace AssetManagement.Models
{
    public class AssetRecord : IAssetRecord
    {
        public IAssetHolder Holder { get; }
        public DateTime Date { get; }
        public AssetState State { get; }

        public AssetRecord(AssetState state, IAssetHolder holder)
        {
            State = state;
            Holder = holder;
            Date = DateTime.Now;
        }
    }
}
