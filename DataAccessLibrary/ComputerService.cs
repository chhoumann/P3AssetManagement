using System;
using System.Collections.Generic;
using System.Linq;
using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.Contexts;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Server
{
    public class ComputerService : AssetService<Computer, ComputerRecord, ComputerContext>
    {
        public ComputerService()
        {
            InsertMockDataToDb(); // TODO: Remove this as soon as we get real data
        }

        protected override ComputerContext Db { get; } = new ComputerContext();
        public override event Action AssetUpdated;

        protected override ComputerRecord InitialRecord(Computer computer) =>
            new ComputerRecord(computer, Depot, DateTime.Now, AssetState.Online);

        private void InsertMockDataToDb()
        {
            if (!Db.Computers.Any())
            {
                List<Computer> newComputers = new List<Computer>();
                for (int i = 0; i < 11; i++)
                {
                    Computer computer = new Computer($"SomeName{i}", "OpSystem", "Manumanu", "Model", "SerialNumber");
                    computer.ComputerRecords.Add(InitialRecord(computer));
                    newComputers.Add(computer);
                }

                Db.AddRange(newComputers);
                Db.SaveChanges();
            }
        }

        protected override void CreateNewAssetRecord(IAsset asset, string assetHolderUsername, DateTime timestamp,
            AssetState state)
        {
            if (asset is Computer computer)
            {
                AssetHolder assetHolder = GetAssetHolderByUsername(assetHolderUsername);
                ComputerRecord newAssetRecord = new ComputerRecord(computer, assetHolder, timestamp, state);
                computer.ComputerRecords.Add(newAssetRecord);
                Db.SaveChanges();
                AssetUpdated?.Invoke();
            }
        }

        protected override void CreateNewAssetRecord(IAsset asset, AssetHolder holder, DateTime timestamp,
            AssetState state)
        {
            if (asset is Computer computer)
            {
                ComputerRecord newAssetRecord = new ComputerRecord(computer, holder, timestamp, state);
                computer.ComputerRecords.Add(newAssetRecord);
                Db.SaveChanges();
                AssetUpdated?.Invoke();
            }
        }

        public override Computer[] GetAssets()
        {
            return Db.Computers
                .Include(computer => computer.ComputerRecords)
                .ThenInclude(record => record.Holder)
                .ToArray();
        }

        public override Computer GetAssetById(string id) => Db.Computers.Find(id);

        public override void DeleteAsset(IAsset asset)
        {
            if (asset is Computer computer)
            {
                Db.Remove(computer);
                Db.SaveChanges();
                AssetUpdated?.Invoke();
            }
        }

        public override void AddAsset(IAsset asset)
        {
            if (asset is Computer computer)
            {
                if (computer.AssetRecords.Count == 0)
                {
                    computer.ComputerRecords.Add(InitialRecord(computer));
                }

                Db.Add(computer);
                Db.SaveChanges();
                AssetUpdated?.Invoke();
            }
        }

        public override void AddAssets(IEnumerable<IAsset> assets)
        {
            int id = 0;
            Console.WriteLine("Do we not get here");
            foreach (IAsset asset in assets)
            {
                if (asset is Computer computer)
                {
                    Console.WriteLine("Yeetin in computer " + ++id);
                    if (asset.AssetRecords.Count == 0)
                    {
                        computer.ComputerRecords.Add(InitialRecord(computer));
                    }

                    Db.Add(computer);
                }
            }

            Db.SaveChanges();
            AssetUpdated?.Invoke();
        }
    }
}