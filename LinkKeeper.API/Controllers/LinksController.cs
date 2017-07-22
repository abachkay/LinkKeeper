using LinkKeeper.API.App_Start;
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
            try
            {
                var id = User.Identity.GetUserId();
                return Content(HttpStatusCode.OK, _linkRepository.GetAll().ToList().Where(l=>l.ApplicationUserId == User.Identity.GetUserId()));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
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
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult Post([FromBody]Link link)
        {
            try
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
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IHttpActionResult Put(int id,[FromBody]Link link)
        {
            try
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
                _linkRepository.Update(newLink);
                _linkRepository.Save();                
                return Content(HttpStatusCode.OK, link);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
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
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/links/filter/{category}")]
        public IHttpActionResult Filter(string category)
        {
            try
            {
                var id = User.Identity.GetUserId();
                var links = _linkRepository.GetAll().ToList().Where(l => l.ApplicationUserId == User.Identity.GetUserId() && l.Category == category);                                
                return Content(HttpStatusCode.OK, links);                                
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/links/categories")]
        public IHttpActionResult Categories()
        {
            try
            {
                var id = User.Identity.GetUserId();
                var categories = _linkRepository.GetAll().ToList().Where(l => l.ApplicationUserId == User.Identity.GetUserId()).Select(l=>l.Category).Distinct();
                return Content(HttpStatusCode.OK, categories);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/links/search/{name}")]
        public IHttpActionResult Search(string name)
        {
            try
            {
                var id = User.Identity.GetUserId();
                var links = _linkRepository.GetAll().ToList().Where(l => l.ApplicationUserId == User.Identity.GetUserId() && l.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
                return Content(HttpStatusCode.OK, links);
            }
            catch
            {
                return BadRequest();
            }
        }

        ~LinksController()
        {
            _linkRepository.Dispose();
        }
    }
}
