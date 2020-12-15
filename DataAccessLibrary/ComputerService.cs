using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.Contexts;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DataModels.Handlers;
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
            AssetOwnershipHandler<Computer, ComputerService>.AssetOwnershipChanged += OnAssetUpdated;
            AssetStateHandler.AssetStateChanged += OnAssetUpdated;
            InsertMockDataToDb(); // TODO: Remove this as soon as we get real data
        }

        /// <summary>
        /// Signals that a computer has been updated in the database.
        /// </summary>
        protected override ComputerContext Db { get; } = new ComputerContext();
        public override event Action AssetUpdated;

        /// <summary>
        /// This method is invoked by a property change in a computer,
        /// which saves the changes to the database and invokes the AssetUpdated event.
        /// </summary>
        protected override void OnAssetUpdated()
        {
            Db.SaveChanges();
            
            AssetUpdated?.Invoke();
        }

        /// <summary>
        /// Gets the initial record for a computer.
        /// </summary>
        /// <param name="computer">The computer which the record refers to</param>
        /// <returns>The initial record present in a computer</returns>
        protected override ComputerRecord InitialRecord(Computer computer) =>
            new ComputerRecord(computer, Depot, DateTime.Now, AssetState.Online);

        private void InsertMockDataToDb()
        {
            if (!Db.Computers.Any())
            {
                List<Computer> newComputers = new List<Computer>();
                for (int i = 0; i < 11; i++)
                {
                    Computer computer = new Computer($"SomeName{i}", "OpSystem", "Manumanu", new List<string>() { "Models" }, "SerialNumber");
                    computer.ComputerRecords.Add(InitialRecord(computer));
                    newComputers.Add(computer);
                }

                Db.AddRange(newComputers);
                Db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all computers from the database.
        /// </summary>
        /// <returns>An array of all computers from the database.</returns>
        public override Computer[] GetAssets()
        {
            return Db.Computers
                .Include(computer => computer.Models)
                .Include(computer => computer.ComputerRecords)
                .ThenInclude(record => record.Holder)
                .ToArray();
        }

        /// <summary>
        /// Gets a computer from the database by the computer's ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The computer with the given ID.</returns>
        public override Computer GetAssetById(string id)
        {
            return Db.Computers
                .Include(computer => computer.ComputerRecords)
                .ThenInclude(record => record.Holder)
                .SingleOrDefault(computer => computer.Id == id);
        }

        public override Computer GetAssetBySerialNumber(string serialNumber)
        {
            return Db.Computers
                .Include(computer => computer.ComputerRecords)
                .ThenInclude(record => record.Holder)
                .SingleOrDefault(computer => computer.SerialNumber == serialNumber);
        }

        /// <summary>
        /// Deletes a computer from the database.
        /// </summary>
        /// <param name="asset">The computer to be deleted.</param>
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

        /// <summary>
        /// Adds a new computer to the database.
        /// </summary>
        /// <param name="assets">The computer to be added</param>
        public override void AddAssets(IEnumerable<Computer> assets)
        {
            Db.AddRange(assets);
            Db.SaveChanges();
            
            AssetUpdated?.Invoke(); 
        }
    }
}