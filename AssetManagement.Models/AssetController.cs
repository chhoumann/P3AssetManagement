using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagement.Models
{
    public static class AssetController
    {        
        public static void TransferOwnership(IAsset assetToTransfer, AssetHolder receiver)
        {
            Asset asset = (Asset)assetToTransfer;
            asset.TransferTo(receiver);
        }

        public static IAsset MakeAsset(int id, string name, string serialNumber)
        {
            return new Asset(id, name, serialNumber);
        }

        // Temporary, I hope. Early mockdata function, pls remove when proper mockdata is done
        public static Task<IAsset[]> GetAssetsAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                Employee anna = new Employee("Anna", "anna@acme.dk");
                Employee ulf = new Employee("Ulf", "ulf@acme.dk");

                List<Asset> assets = new List<Asset>()
                {
                    new Asset(69420, "Epic Dell Gaming PC", "SN1"),
                    new Asset(1234, "Not So Epic Dell Gaming PC", "SN1"),
                    new Asset(4, "HP PC", "SN1"),
                };

                TransferOwnership(assets[0], ulf);
                TransferOwnership(assets[1], ulf);
                TransferOwnership(assets[2], anna);

                return (IAsset[])assets.ToArray();
            });
        }

        private sealed class Asset : IAsset
        {
            public string Model { get; }
            public string SerialNumber { get; }

            public int Id { get; }

            public DateTime LastChanged { get; private set; }

            public AssetHolder CurrentAssetHolder { get; private set; }
            public StateRecord State { get; private set; }

            public List<Transaction> Transactions { get; } = new List<Transaction>();

            public Asset(int id, string name, string serialNumber)
            {
                Model = name;
                SerialNumber = serialNumber;
                Id = id;

                State = new StateRecord(AssetState.Recovered);
            }

            public void TransferTo(AssetHolder newAssetHolder)
            {

                if (newAssetHolder.CurrentAssets.Contains(this))
                {
                    // ERROR: New holder is somehow already holding this asset!
                    throw new ArgumentException("Attempt to add an asset to an asset holder's asset list which already contains this asset!", Model);
                }

                // Remove this asset from its current holder, if any
                if (CurrentAssetHolder != null)
                {
                    if (!CurrentAssetHolder.CurrentAssets.Contains(this))
                    {
                        // ERROR: This asset does not exist in the CurrentAssetHolder's list so we cannot remove it!
                        throw new ArgumentException($"Attempt to remove asset \"{Model}\" from an asset holder's asset list that did not contain the asset!");
                    }

                    CurrentAssetHolder.CurrentAssets.Remove(this);
                }

                

                // Update the current holder by transferring this asset to the new AssetHolder
                newAssetHolder.CurrentAssets.Add(this);
                CurrentAssetHolder = newAssetHolder;
            }

            public void Dispose() { }
        }
    }
}
