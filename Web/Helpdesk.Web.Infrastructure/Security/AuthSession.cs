using Helpdesk.Dominio.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Infrastructure.Security
{
    public class AuthSession
    {
        public int Codigo { get; set; }

        public string Nome { get; set; }

        public UsuarioTipo Tipo { get; set; }
    }
}
