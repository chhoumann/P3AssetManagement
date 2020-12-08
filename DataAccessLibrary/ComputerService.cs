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
            InsertMockDataToDb(); // TODO: Remove this as soon as we get real data
        }

        public override event Action AssetUpdated;

        protected override void OnAssetUpdated()
        {
            Db.SaveChanges();
            AssetUpdated?.Invoke();
        }

        protected override ComputerContext Db { get; } = new ComputerContext();

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

        public override void DeleteAsset(Computer asset)
        {
            Db.Remove(asset);
            Db.SaveChanges();
            AssetUpdated?.Invoke(); 
        }

        public override void AddAsset(Computer asset)
        {
            if (asset.AssetRecords.Count == 0)
            {
                asset.ComputerRecords.Add(InitialRecord(asset));
            }

            Db.Add(asset);
            Db.SaveChanges();
            AssetUpdated?.Invoke();
        }

        public override void AddAssets(IEnumerable<Computer> assets)
        {
            Db.AddRange(assets);
            Db.SaveChanges();
            AssetUpdated?.Invoke(); 
        }
    }
}