using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AssetManagement.Models;

namespace AssetManagement
{
    public class AssetService
    {
        public Task<Asset[]> GetAssetsAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                OnlineAsset onlineAsset = new OnlineAsset("69420", "Epic Dell Gaming PC");
                AnalogueAsset analogueAsset = new AnalogueAsset("Very Expensive Headset");

                Employee employee = new Employee("Ulf", "ulf@acme.dk");

                onlineAsset.TransferTo(employee);
                analogueAsset.TransferTo(employee);

                return new Asset[] { onlineAsset, analogueAsset };
            });
        }
    }
}
