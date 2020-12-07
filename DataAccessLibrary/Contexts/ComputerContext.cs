using AssetManagement.DataAccessLibrary.DataModels;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.DataAccessLibrary.Contexts
{
    public class ComputerContext : AssetContext
    {
        public DbSet<ComputerRecord> ComputerRecords { get; set; }
        public DbSet<Computer> Computers { get; set; }
    }
}