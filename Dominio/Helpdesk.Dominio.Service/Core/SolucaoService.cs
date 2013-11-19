using Helpdesk.Dominio.Core.Core;
using Helpdesk.Dominio.Data.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Dominio.Service.Core
{
    public class SolucaoService
    {
        public Solucao FindById(int id)
        {
            return new SolucaoRepository().FindById(id);
        }

        public Solucao Salvar(Solucao solucao)
        {
            new SolucaoRepository().Add(solucao);
            return solucao;
        }

        public int TotalReferencias(int id)
        {
            return new SolucaoRepository().TotalReferencias(id);
        }
    }
}
