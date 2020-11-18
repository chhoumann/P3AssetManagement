using Microsoft.EntityFrameworkCore;
using AssetManagement.Models.Asset;

namespace AssetManagement.DataAccessLibrary.DbContexts
{
    public sealed class AssetContext : P3DbContext
    {
        public DbSet<Asset> AssetData { get; set; }
    }
}
