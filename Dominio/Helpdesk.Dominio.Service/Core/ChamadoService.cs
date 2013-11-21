using Helpdesk.Dominio.Core.Core;
using Helpdesk.Dominio.Data.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Dominio.Service.Core
{
    public class ChamadoService
    {
        public IList<Chamado> GetChamados()
        {
            return new ChamadoRepository().GetChamados();
        }

        public IList<Chamado> GetChamadosWithSolucao()
        {
            return new ChamadoRepository().GetChamadosWithSolucao();
        }

        public IList<Chamado> GetChamadosEmAberto()
        {
            return new ChamadoRepository().GetChamadosEmAberto();
        }

        public Chamado Salvar(Chamado chamado)
        {
            new ChamadoRepository().Add(chamado);
            return chamado;
        }

        public Chamado FindById(int codigo)
        {
            return new ChamadoRepository().FindById(codigo);
        }
    }
}
