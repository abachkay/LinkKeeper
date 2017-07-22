using LinkKeeper.DAL;
using LinkKeeper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkKeeper.BLL
{
    public interface ILinksService: IDisposable
    {
        IList<Link> GetLinks(string userId);
        Link GetLinkById(string userId,int linkId);
        void CreateLink(string userId, Link link);
        void UpdateLink(string userId, int linkId, Link link);
        void DeleteLink(string userId, int linkId);
        IList<Link> FilterLinksByCategory(string userId, string category);
        IList<string> GetCategories(string userId);
    }
}
