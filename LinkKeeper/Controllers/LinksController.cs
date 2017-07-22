using LinkKeeper.API.App_Start;
using LinkKeeper.API.Filters;
using LinkKeeper.BLL;
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
        ILinksService _linksService;
        public LinksController(ILinksService linksService)
        {                        
            _linksService = linksService;
        }
        [HttpGet]        
        public IHttpActionResult Get()
        {                                   
            return Content(HttpStatusCode.OK, _linksService.GetLinks(User.Identity.GetUserId()));         
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Content(HttpStatusCode.OK, _linksService.GetLinkById(User.Identity.GetUserId(), id));
        }
        [HttpPost]
        public IHttpActionResult Post([FromBody]Link link)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _linksService.CreateLink(User.Identity.GetUserId(), link);
            return Created($"api/links/{link.LinkId}", link);
        }
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Link link)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _linksService.UpdateLink(User.Identity.GetUserId(), id, link);
            return Content(HttpStatusCode.OK, link);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _linksService.DeleteLink(User.Identity.GetUserId(), id);
            return Ok();
        }

        [HttpGet]
        [Route("api/links/filter/{category}")]
        public IHttpActionResult Filter(string category)
        {
            return Content(HttpStatusCode.OK, _linksService.FilterLinksByCategory(User.Identity.GetUserId(), category));
        }

        [HttpGet]
        [Route("api/links/categories")]
        public IHttpActionResult Categories()
        {
            return Content(HttpStatusCode.OK, _linksService.GetCategories(User.Identity.GetUserId()));
        }  
    }
}
