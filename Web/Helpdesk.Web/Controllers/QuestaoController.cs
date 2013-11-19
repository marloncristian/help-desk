using Helpdesk.Web.Filters;
using Helpdesk.Web.Models.QuestaoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Helpdesk.Web.Controllers
{
    public class QuestaoController : Controller
    {
        [Authenticate]
        public ActionResult Index()
        {
            return View(new QuestaoIndexModel());
        }

        [Authenticate]
        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult Pesquisar()
        {
            var model = new QuestaoIndexModel();
            model.Respostas.Add(new QuestaoRespostaModel() { Texto = "Mussum ipsum cacilds, vidis litro abertis. Consetis adipiscings elitis."});
            model.Respostas.Add(new QuestaoRespostaModel() { Texto = "Mussum ipsum cacilds, vidis litro abertis. Consetis adipiscings elitis."});
            model.Respostas.Add(new QuestaoRespostaModel() { Texto = "Mussum ipsum cacilds, vidis litro abertis. Consetis adipiscings elitis." });
            return View("Index", model);
        }

    }
}
