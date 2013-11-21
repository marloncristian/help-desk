using FluentNHibernate.Mapping;
using Helpdesk.Dominio.Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Infrastructure.Mapping
{
    public class SolucaoMap : ClassMap<Solucao>
    {
        public SolucaoMap()
        {
            Id(o => o.Codigo);
            Map(o => o.Descricao).Column("Descricao");
        }
    }
}
