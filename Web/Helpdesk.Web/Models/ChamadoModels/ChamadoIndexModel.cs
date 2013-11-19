using Helpdesk.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Helpdesk.Web.Models.ChamadoModels
{
    public class ChamadoIndexModel
    {
        public ChamadoIndexModel()
        {
        }

        [Required]
        public string Assunto { get; set; }

        [Required]
        public string Problema { get; set; }

        public String[] Chaves
        {
            get
            {
                return 
                    Assunto.ToLower().Split().Union(Problema.ToLower().Split())
                    .Where(o => !ChamadoEngine.stopWords.Contains(o)).ToArray();
            }
        }

        public IList<SolucaoModel> Solucoes { get; set; }
    }
}