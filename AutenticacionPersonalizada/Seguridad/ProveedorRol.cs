using System;
using System.Configuration;
using System.Linq;
using System.Web.Security;
using AutenticacionPersonalizada.Models;
using AutenticacionPersonalizada.Utilities;

namespace AutenticacionPersonalizada.Seguridad
{
    public class ProveedorRol : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            string clave = ConfigurationManager.AppSettings["mikey"];
            string cif = SeguridadUtilities.Cifrar(username, clave);
            using (AuthenticationEntities db = new AuthenticationEntities())
            {
                Usuario usuario = db.Usuario.First(o => o.Login == cif);
                try
                {
                    return usuario.Rol.Nombre == roleName;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            string clave = ConfigurationManager.AppSettings["mikey"];
            string cif = SeguridadUtilities.Cifrar(username, clave);
            using (AuthenticationEntities db = new AuthenticationEntities())
            {
                Usuario usuario = db.Usuario.First(o => o.Login == cif);
                try
                {
                    return new [] { usuario.Rol.Nombre }; 
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}
