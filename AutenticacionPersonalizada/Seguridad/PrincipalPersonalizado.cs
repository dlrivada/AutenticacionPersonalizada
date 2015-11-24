using System.Security.Principal;

namespace AutenticacionPersonalizada.Seguridad
{
    public class PrincipalPersonalizado : IPrincipal
    {
        public bool IsInRole(string role)
        {
            return MiIdentityPersonalizado.Rol == role;
        }

        public IIdentity Identity { get; private set; }

        public IdentityPersonalizado MiIdentityPersonalizado
        {
            get { return Identity as IdentityPersonalizado; }
            set { Identity = value; }
        }

        public PrincipalPersonalizado(IdentityPersonalizado identityPersonalizado)
        {
            Identity = identityPersonalizado;
        }
    }
}
