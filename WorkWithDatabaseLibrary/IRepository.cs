using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDatabaseLibrary
{
    public interface IRepository<T> : IDisposable where T : class
    {
        // Create object
        Task<int> CreateAsync(T item);
    }
}
