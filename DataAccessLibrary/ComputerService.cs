using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.Contexts;
using AssetManagement.DataAccessLibrary.DataModels;
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

        /// <summary>
        /// Signals that an computer has been updated in the database.
        /// </summary>
        public override event Action AssetUpdated;

        protected override ComputerContext Db { get; } = new ComputerContext();

        public static ComputerService Instance { get; private set; }

        /// <summary>
        /// This method is invoked by a property change in an computer,
        /// which saves the changes to the database and invokes the AssetUpdated event.
        /// </summary>
        protected override void OnAssetUpdated()
        {
            Db.SaveChanges();
            AssetUpdated?.Invoke();
        }

        /// <summary>
        /// Gets the initial record for an computer.
        /// </summary>
        /// <param name="computer">The computer which the record refers to</param>
        /// <returns>The initial record present in an computer</returns>
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

        /// <summary>
        /// Gets all computers from the database.
        /// </summary>
        /// <returns>An array of all computers from the database.</returns>
        public override Computer[] GetAssets()
        {
            return Db.Computers
                .Include(computer => computer.ComputerRecords)
                .ThenInclude(record => record.Holder)
                .ToArray();
        }

        /// <summary>
        /// Gets an computer by the computer's ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The computer with the given ID.</returns>
        public override Computer GetAssetById(string id) => Db.Computers.Find(id);

        /// <summary>
        /// Deletes an computer from the database.
        /// </summary>
        /// <param name="computer">The computer to be deleted.</param>
        public override void DeleteAsset(Computer computer)
        {
            Db.Remove(computer);
            Db.SaveChanges();
            AssetUpdated?.Invoke();
        }

        /// <summary>
        /// Adds a new computer to the database.
        /// </summary>
        /// <param name="computer">The computer to be added</param>
        public override void AddAsset(Computer computer)
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
}