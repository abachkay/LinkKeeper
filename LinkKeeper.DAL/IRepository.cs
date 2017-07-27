using System;
using System.Collections.Generic;

namespace LinkKeeper.DAL
{
    public interface IRepository<T> : IDisposable         
    {
        IEnumerable<T> GetAll(); 
        T GetById(int id); 
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        void Save();
    }
}
