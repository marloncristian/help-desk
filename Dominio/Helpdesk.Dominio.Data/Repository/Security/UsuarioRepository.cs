using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpdesk.Dominio.Core.Security;
using Republica.Middleware.Repository;

namespace Helpdesk.Dominio.Data.Repository.Security
{
    public class UsuarioRepository : Repository<Usuario>
    {
        public IList<Usuario> GetUsuarios()
        {
            return this.Session.QueryOver<Usuario>().List();
        }

        public Usuario GetUsuario(string login, string senha)
        {
            return this.Session.QueryOver<Usuario>()
                .Where(o => o.Login == login && o.Senha == senha)
                .SingleOrDefault();
        }

        public Usuario GetUsuario(string login)
        {
            return this.Session.QueryOver<Usuario>()
                .Where(o => o.Login == login)
                .List().FirstOrDefault();
        }
    }
}
