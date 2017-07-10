using LinkKeeper.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LinkKeeper.DAL
{
    public class SqlLinkRepository : IRepository<Link>
    {
        LinkKeeperDbContext _context = new LinkKeeperDbContext();
        public void Create(Link item)
        {            
            _context.Links.Add(item);
        }

        public void Delete(Link item)
        {
            _context.Links.Remove(item);
        }

        public IEnumerable<Link> GetAll()
        {
            return _context.Links.ToList();
        }

        public Link GetById(int id)
        {
            return _context.Links.Find(id);
        }

        public void Update(Link item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }               
                disposedValue = true;
            }
        }      
        public void Dispose()
        {            
            Dispose(true);            
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
