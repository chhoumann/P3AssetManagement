using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagement.DataAccessLibrary.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public DbContext context;
        public DbSet<T> table;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }

        public async Task Insert(T obj)
        {
            await table.AddAsync(obj);
        }

        public void Update(T obj)
        {
            table.Update(obj);
            context.Entry(obj).State = EntityState.Modified;
        }

        public async Task Delete(object id)
        {
            T existing = await table.FindAsync(id);
            table.Remove(existing);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
