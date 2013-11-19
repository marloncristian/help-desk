using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helpdesk.Web.Models.QuestaoModel
{
    public class QuestaoIndexModel
    {
        public QuestaoIndexModel()
        {
            Respostas = new List<QuestaoRespostaModel>();
        }

        public string Assunto { get; set; }

        public string Problema { get; set; }

        public IList<QuestaoRespostaModel> Respostas { get; set; }
    }
}