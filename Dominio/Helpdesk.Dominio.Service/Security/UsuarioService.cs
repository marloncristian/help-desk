using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpdesk.Dominio.Core.Security;
using Helpdesk.Dominio.Data.Repository.Security;

namespace Helpdesk.Dominio.Service.Security
{
    public class UsuarioService
    {
        public Usuario Save(Usuario usuario)
        {
            new UsuarioRepository().Add(usuario);
            return usuario;
        }

        public IList<Usuario> GetUsuarios()
        {
            return new UsuarioRepository().GetUsuarios();
        }

        public Usuario GetUsuario(string usuario, string senha)
        {
            return new UsuarioRepository().GetUsuario(usuario, senha);
        }

        public Usuario GetUsuario(string login)
        {
            return new UsuarioRepository().GetUsuario(login);
        }

        public Usuario GetUsuario(int codigo)
        {
            return new UsuarioRepository().FindById(codigo);
        }

        public void RemoveUsuario(int codigo)
        {
            var repository = new UsuarioRepository();
            var usuario = repository.FindById(codigo);
            new UsuarioRepository().Remove(usuario);
        }
    }
}
