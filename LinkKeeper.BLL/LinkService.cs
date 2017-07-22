using LinkKeeper.DAL;
using LinkKeeper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkKeeper.BLL
{
    public class LinkService: ILinksService 
    {            
        public IList<Link> GetLinks(string userId)
        {
            using (var linkRepository = new SqlLinkRepository())
            {
                return linkRepository.GetAll().ToList().Where(l => l.ApplicationUserId == userId).ToList();
            }
        }

        public Link GetLinkById(string userId, int linkId)
        {
            using (var linkRepository = new SqlLinkRepository())
            {
                var link = linkRepository.GetById(linkId);
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
        }

        public void CreateLink(string userId, Link link)
        {
            using (var linkRepository = new SqlLinkRepository())
            {
                link.ApplicationUserId = userId;
                linkRepository.Create(link);
                linkRepository.Save();
            }
        }

        public void UpdateLink(string userId, int linkId, Link link)
        {
            using (var linkRepository = new SqlLinkRepository())
            {
                var newLink = linkRepository.GetById(linkId);
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
                linkRepository.Update(newLink);
                linkRepository.Save();
            }
        }

        public void DeleteLink(string userId, int linkId)
        {
            using (var linkRepository = new SqlLinkRepository())
            {
                var link = linkRepository.GetById(linkId);
                if (link == null)
                {
                    throw new ArgumentException("No links with given id");
                }
                if (link.ApplicationUserId != userId)
                {
                    throw new UnauthorizedAccessException("This user is not authorized for operation");
                }
                linkRepository.Delete(link);
                linkRepository.Save();
            }
        }

        public IList<Link> FilterLinksByCategory(string userId, string category)
        {
            using (var linkRepository = new SqlLinkRepository())
            {
                return linkRepository.GetAll().ToList().Where(l => l.ApplicationUserId == userId && l.Category == category).ToList();
            }
        }

        public IList<string> GetCategories(string userId)
        {
            using (var linkRepository = new SqlLinkRepository())
            {
                return linkRepository.GetAll().ToList().Where(l => l.ApplicationUserId == userId).Select(l => l.Category).Distinct().ToList();
            }
        }      
    }
}
