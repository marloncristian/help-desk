using Helpdesk.Dominio.Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helpdesk.Web.Models.ChamadoModels
{
    public class ChamadoModel
    {
        public int Codigo { get; set; }

        public ChamadoStatus Status { get; set; }

        public DateTime Abertura { get; set; }

        public String Assunto { get; set; }

        public String Problema { get; set; }

        public String Solucao { get; set; }
    }
}