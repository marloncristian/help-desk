using Helpdesk.Dominio.Core.Core;
using Republica.Middleware.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Dominio.Data.Repository.Core
{
    public class ChamadoRepository : Repository<Chamado>
    {
        public IList<Chamado> GetChamados()
        {
            return this.Session.QueryOver<Chamado>().List();
        }

        public IList<Chamado> GetChamadosWithSolucao()
        {
            return this.Session.QueryOver<Chamado>().Where(o => o.Solucao != null).List();
        }

        public IList<Chamado> GetChamadosEmAberto()
        {
            return this.Session.QueryOver<Chamado>().Where(o => o.Status == ChamadoStatus.Aberto && o.Solucao == null).List();
        }
    }
}
