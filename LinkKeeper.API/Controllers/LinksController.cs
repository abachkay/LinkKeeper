using LinkKeeper.DAL;
using LinkKeeper.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace LinkKeeper.API.Controllers
{    
    [Authorize]
    public class LinksController : ApiController
    {
        IRepository<Link> _linkRepository = new SqlLinkRepository();
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

        ~LinksController()
        {
            _linkRepository.Dispose();
        }
    }
}
