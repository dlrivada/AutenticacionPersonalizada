using System.Configuration;
using System.Web.Security;
using AutenticacionPersonalizada.Models;
using AutenticacionPersonalizada.Utilities;

namespace AutenticacionPersonalizada.Seguridad
{
    public class UsuarioMembership: MembershipUser
    {
        public int IdUsuario { get; set; }
        public string Login { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Imagen { get; set; }
        public string Rol { get; set; }

        public UsuarioMembership(Usuario usuario)
        {
            string clave = ConfigurationManager.AppSettings["mikey"];
            IdUsuario = usuario.IdUsuario;
            Login = SeguridadUtilities.Descifrar(usuario.Login, clave);
            Nombre = usuario.Nombre;
            Apellidos = usuario.Apellidos;
            Imagen = usuario.Imagen;

        }
    }
}
