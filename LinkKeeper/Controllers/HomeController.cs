using System.Web.Mvc;

namespace LinkKeeper.API.Controllers
{    
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {                     
            return View();
        }        
        [Route("{*url}")]
        public ActionResult Other()
        {
            return RedirectToAction("Index");
        }
    }
}
