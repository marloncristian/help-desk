using Helpdesk.Dominio.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Dominio.Core.Core
{
    public class Chamado
    {
        public virtual int Codigo { get; set; }

        public virtual ChamadoStatus Status { get; set; }

        public virtual DateTime Abertura { get; set; }

        public virtual String Assunto { get; set; }

        public virtual String Problema { get; set; }

        public virtual String Chaves { get; set; }

        public virtual Solucao Solucao { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
