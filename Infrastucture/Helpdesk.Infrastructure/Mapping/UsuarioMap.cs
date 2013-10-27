using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Helpdesk.Dominio.Core.Security;

namespace Helpdesk.Infrastructure.Mapping
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Id(o => o.Codigo);
            Map(o => o.Nome).Column("Nome");
            Map(o => o.Login).Column("Login");
            Map(o => o.Senha).Column("Senha");
        }
    }
}
