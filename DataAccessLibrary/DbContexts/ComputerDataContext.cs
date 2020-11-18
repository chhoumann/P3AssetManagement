using AssetManagement.DataAccessLibrary.DataModels;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.DataAccessLibrary.DbContexts
{
    public sealed class ComputerDataContext : P3DbContext
    {
        public DbSet<ComputerData> ComputerData { get; set; }
    }
}