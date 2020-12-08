using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.Contexts;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetManagement.DataAccessLibrary
{
    public class ComputerService : AssetService<Computer, ComputerRecord, ComputerContext>
    {
        public ComputerService()
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("ComputerService instance already exists. Only one instance may exist.");
            }
            Instance = this;
            InsertMockDataToDb(); // TODO: Remove this as soon as we get real data
        }

        public override event Action AssetUpdated;

        protected override void OnAssetUpdated()
        {
            Db.SaveChanges();
            AssetUpdated?.Invoke();
        }

        protected override ComputerContext Db { get; } = new ComputerContext();

        public static ComputerService Instance { get; private set; }

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