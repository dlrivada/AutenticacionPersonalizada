using System.Security.Principal;
using System.Web.Security;

namespace AutenticacionPersonalizada.Seguridad
{
    public class IdentityPersonalizado : IIdentity
    {
        public string Name => Login;

        public string AuthenticationType => Identity.AuthenticationType;

        public bool IsAuthenticated => Identity.IsAuthenticated;
        public int IdUsuario { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Imagen { get; set; }
        public string Rol { get; set; }
        public IIdentity Identity { get; set; }

        public IdentityPersonalizado(IIdentity identity)
        {
            Identity = identity;
            UsuarioMembership usuario = Membership.GetUser(identity.Name) as UsuarioMembership;
            if (usuario == null)
                return;
            Nombre = usuario.Nombre;
            Apellidos = usuario.Apellidos;
            Login = usuario.Login;
            IdUsuario = usuario.IdUsuario;
            Imagen = usuario.Imagen;
            Rol = usuario.Rol;
        }
    }
}
