using System.Web;
using System.Web.Mvc;

namespace LinkKeeper.API.Controllers
{    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {                     
            return View();
        }        
        [Route("links")]
        public ActionResult Links()
        {
            return RedirectPermanent("/#!/links");
        }
        [Route("login")]
        public ActionResult Login()
        {
            return RedirectPermanent("/#!/login");
        }
        [Route("register")]
        public ActionResult Register()
        {
            return RedirectPermanent("/#!/register");
        }
    }
}
