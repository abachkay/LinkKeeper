using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
