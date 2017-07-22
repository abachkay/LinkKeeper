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
    }
}
