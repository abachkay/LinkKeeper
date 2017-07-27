using LinkKeeper.DAL;
using LinkKeeper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkKeeper.BLL
{
    public class LinkService: ILinksService 
    {
        private IRepository<Link> _linkRepository;
        public LinkService(IRepository<Link>  linkRepository)
        {
            _linkRepository = linkRepository;
        }
        public IList<Link> GetLinks(string userId)
        {   
            return _linkRepository.GetAll().ToList().Where(l => l.ApplicationUserId == userId).ToList();         
        }

        public Link GetLinkById(string userId, int linkId)
        {
            var link = _linkRepository.GetById(linkId);
            if (link == null)
            {
                throw new ArgumentException("No links with given id");
            }
            if (link.ApplicationUserId != userId)
            {
                throw new UnauthorizedAccessException("This user is not authorized for operation");
            }
            return link;
        }

        public void CreateLink(string userId, Link link)
        {            
            link.ApplicationUserId = userId;
            _linkRepository.Create(link);
            _linkRepository.Save();            
        }

        public void UpdateLink(string userId, int linkId, Link link)
        {           
            var newLink = _linkRepository.GetById(linkId);
            if (newLink == null)
            {
                throw new ArgumentException("No links with given id");
            }
            if (newLink.ApplicationUserId != userId)
            {
                throw new UnauthorizedAccessException("This user is not authorized for operation");
            }
            newLink.Url = link.Url;
            newLink.Name = link.Name;
            newLink.Category = link.Category;
            newLink.IsFavorite = link.IsFavorite;
            _linkRepository.Update(newLink);
            _linkRepository.Save();            
        }

        public void DeleteLink(string userId, int linkId)
        {
            var link = _linkRepository.GetById(linkId);
            if (link == null)
            {
                throw new ArgumentException("No links with given id");
            }
            if (link.ApplicationUserId != userId)
            {
                throw new UnauthorizedAccessException("This user is not authorized for operation");
            }
            _linkRepository.Delete(link);
            _linkRepository.Save();
        }

        public IList<Link> FilterLinksByCategory(string userId, string category)
        {           
            return _linkRepository.GetAll().ToList().Where(l => l.ApplicationUserId == userId && l.Category == category).ToList();            
        }

        public IList<string> GetCategories(string userId)
        {
            return _linkRepository.GetAll().ToList().Where(l => l.ApplicationUserId == userId).Select(l => l.Category).Distinct().ToList();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _linkRepository.Dispose();
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
