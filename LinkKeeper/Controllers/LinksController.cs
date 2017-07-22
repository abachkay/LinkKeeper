using LinkKeeper.API.App_Start;
using LinkKeeper.API.Filters;
using LinkKeeper.DAL;
using LinkKeeper.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace LinkKeeper.API.Controllers
{    
    [Authorize]
    [BadRequestException]
    public class LinksController : ApiController
    {
        IRepository<Link> _linkRepository;
        public LinksController(IRepository<Link> linkRepository)
        {            
            _linkRepository = linkRepository;            
        }
        [HttpGet]        
        public IHttpActionResult Get()
        {                       
            var id = User.Identity.GetUserId();
            return Content(HttpStatusCode.OK, _linkRepository.GetAll().ToList().Where(l=>l.ApplicationUserId == User.Identity.GetUserId()));         
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {   
            var link = _linkRepository.GetById(id);
            if (link == null)
            {
                return BadRequest();
            }
            if (link.ApplicationUserId != User.Identity.GetUserId())
            {
                return Unauthorized();
            }
            return Content(HttpStatusCode.OK, link);          
        }
        [HttpPost]
        public IHttpActionResult Post([FromBody]Link link)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            link.ApplicationUserId = User.Identity.GetUserId();
            _linkRepository.Create(link);
            _linkRepository.Save();
            return Created($"api/links/{link.LinkId}", link);
        }
        [HttpPut]
        public IHttpActionResult Put(int id,[FromBody]Link link)
        {          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newLink = _linkRepository.GetById(id);
            if (newLink.ApplicationUserId != User.Identity.GetUserId())
            {
                return Unauthorized();
            }
            newLink.Url = link.Url;
            newLink.Name = link.Name;
            newLink.Category = link.Category;
            newLink.IsFavorite = link.IsFavorite;
            _linkRepository.Update(newLink);
            _linkRepository.Save();                
            return Content(HttpStatusCode.OK, link);   
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {        
            var link = _linkRepository.GetById(id);
            if (link.ApplicationUserId != User.Identity.GetUserId())
            {
                return Unauthorized();
            }
            _linkRepository.Delete(link);
            _linkRepository.Save();
            return Content(HttpStatusCode.OK, link);       
        }

        [HttpGet]
        [Route("api/links/filter/{category}")]
        public IHttpActionResult Filter(string category)
        {        
            var id = User.Identity.GetUserId();
            var links = _linkRepository.GetAll().ToList().Where(l => l.ApplicationUserId == User.Identity.GetUserId() && l.Category == category);                                
            return Content(HttpStatusCode.OK, links);                                      
        }

        [HttpGet]
        [Route("api/links/categories")]
        public IHttpActionResult Categories()
        {       
            var id = User.Identity.GetUserId();
            var categories = _linkRepository.GetAll().ToList().Where(l => l.ApplicationUserId == User.Identity.GetUserId()).Select(l=>l.Category).Distinct();
            return Content(HttpStatusCode.OK, categories);       
        }

        [HttpGet]
        [Route("api/links/search/{name}")]
        public IHttpActionResult Search(string name)
        {          
            var id = User.Identity.GetUserId();
            var links = _linkRepository.GetAll().ToList().Where(l => l.ApplicationUserId == User.Identity.GetUserId() && l.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
            return Content(HttpStatusCode.OK, links);     
        }

        ~LinksController()
        {
            _linkRepository.Dispose();
        }
    }
}
