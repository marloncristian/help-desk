using Helpdesk.Dominio.Core.Core;
using Republica.Middleware.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Dominio.Data.Repository.Core
{
    public class SolucaoRepository : Repository<Solucao>
    {
        public int TotalReferencias(int codigo)
        {
            return this.Session.QueryOver<Chamado>().JoinQueryOver(o => o.Solucao).Where(o => o.Codigo == codigo).RowCount();
        }
    }
}
