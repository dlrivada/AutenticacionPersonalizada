using System.Web.Mvc;

namespace AutenticacionPersonalizada.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}