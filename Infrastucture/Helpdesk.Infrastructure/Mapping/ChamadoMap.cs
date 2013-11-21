using FluentNHibernate.Mapping;
using Helpdesk.Dominio.Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Infrastructure.Mapping
{
    public class ChamadoMap : ClassMap<Chamado>
    {
        public ChamadoMap()
        {
            Id(o => o.Codigo);
            Map(o => o.Status).Column("Status").CustomType(typeof(ChamadoStatus));
            Map(o => o.Abertura).Column("Abertura");
            Map(o => o.Assunto).Column("Assunto");
            Map(o => o.Problema).Column("Problema");
            Map(o => o.Chaves).Column("Chaves");
            References(o => o.Solucao).Nullable().Column("Solucao");
            References(o => o.Usuario).Nullable().Column("Usuario");
        }
    }
}
