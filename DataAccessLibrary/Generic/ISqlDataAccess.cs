﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagement.DataAccessLibrary.Generic
{
    public interface ISqlDataAccess<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(object id);
        Task Insert(T obj);
        Task InsertRange(IEnumerable<T> entities);
        void Update(T obj);
        Task Delete(object id);
        Task Save();
    }
}
