using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutenticacionPersonalizada.Seguridad;

namespace AutenticacionPersonalizada
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                IdentityPersonalizado identity = new IdentityPersonalizado(HttpContext.Current.User.Identity);
                PrincipalPersonalizado principal = new PrincipalPersonalizado(identity);
                HttpContext.Current.User = principal;
            }
        }
    }
}
