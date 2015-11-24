using System.Web.Mvc;
using System.Web.Security;
using AutenticacionPersonalizada.Models;

namespace AutenticacionPersonalizada.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario model)
        {
            if (!Membership.ValidateUser(model.Login, model.Password))
                return View(model);
            FormsAuthentication.RedirectFromLoginPage(model.Login, false);
            return null;
        }
    }
}