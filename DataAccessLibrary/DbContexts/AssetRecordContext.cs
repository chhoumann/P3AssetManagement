using AssetManagement.DataAccessLibrary.DataModels;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.DataAccessLibrary.DbContexts
{
    public sealed class AssetRecordContext : P3DbContext
    {
        public DbSet<AssetRecordData> AssetRecordData { get; set; }
    }
}