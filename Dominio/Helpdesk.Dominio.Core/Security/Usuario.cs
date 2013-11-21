using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Dominio.Core.Security
{
    public class Usuario
    {
        public virtual int Codigo { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Login { get; set; }

        public virtual string Senha { get; set; }

        public virtual UsuarioTipo Tipo { get; set; }
    }
}
