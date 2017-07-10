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
        public ActionResult Links()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Register");
            }
            return View();
        }
        public ActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }           
            return RedirectToAction("Index");           
        }        
        public ActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
