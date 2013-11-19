using Helpdesk.Dominio.Core.Core;
using Helpdesk.Dominio.Service.Core;
using Helpdesk.Dominio.Service.Security;
using Helpdesk.Infrastructure.Security;
using Helpdesk.Web.Infrastructure.Core;
using Helpdesk.Web.Models.ChamadoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Helpdesk.Web.Controllers
{
    public class ChamadoController : Controller
    {
        //
        // GET: /Chamado/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Visualizar(int codigo)
        {
            var chamado = new ChamadoService().FindById(codigo);
            var model = new ChamadoModel()
            {
                Abertura = chamado.Abertura,
                Assunto = chamado.Assunto,
                Codigo = chamado.Codigo,
                Problema = chamado.Problema,
                Status = chamado.Status
            };
            return View(model);
        }

        public ActionResult Solucionar(ChamadoModel model)
        {
            if (String.IsNullOrEmpty(model.Solucao))
            {
                ViewBag.MensagemErro = "Indique uma solução para o problema";
                return View("Visualizar");
            }

            var solucaoService = new SolucaoService();
            var chamadoService = new ChamadoService();

            var solucao = new Solucao();
            solucao.Descricao = model.Solucao;
            solucaoService.Salvar(solucao);

            var chamado = chamadoService.FindById(model.Codigo);
            chamado.Solucao = solucao;
            chamado.Status = ChamadoStatus.Fechado;
            chamadoService.Salvar(chamado);

            ViewBag.MensagemSucesso = "Sua solução foi registrada com sucesso.";
            return View("Visualizar",
                new ChamadoModel()
                {
                    Abertura = chamado.Abertura,
                    Assunto = chamado.Assunto,
                    Codigo = chamado.Codigo,
                    Problema = chamado.Problema,
                    Status = chamado.Status
                });
        }

        public ActionResult Pesquisar()
        {
            return View(new ChamadoIndexModel());
        }

        [HttpPost]
        public ActionResult Pesquisar(ChamadoIndexModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MensagemErro = "Preencha os campos Assunto e Problema para prosseguir";
                return View("Pesquisar", model);
            }

            var solucaoService = new SolucaoService();

            var chamados = new ChamadoService().GetChamadosWithSolucao();
            model.Solucoes =
                ChamadoEngine.OrderByLevenshtein(model.Chaves, chamados, 3)
                .OrderByDescending(o => solucaoService.TotalReferencias(o.Codigo))
                .Select(
                    o => new SolucaoModel
                    {
                        Codigo = o.Codigo,
                        Descricao = o.Descricao
                    }).ToList();

            return View(model);
        }

        public ActionResult Abertos()
        {
            var model = new ChamadoVisualizarModel();
            model.Chamados = new ChamadoService().GetChamadosEmAberto()
                .Select(o => new ChamadoModel() {
                    Abertura = o.Abertura,
                    Assunto = o.Assunto,
                    Codigo = o.Codigo,
                    Problema = o.Problema,
                    Status = o.Status
                }).ToList();
            return View(model);
        }

        public ActionResult Adicionar(ChamadoIndexModel model, int? solucao)
        {
            var chamado = new Chamado()
            {
                Abertura = DateTime.Now,
                Assunto = model.Assunto,
                Chaves = String.Join(" ", model.Chaves),
                Problema = model.Problema
            };

            if (AuthContext.IsConnected)
            {
                var usuario = new UsuarioService().GetUsuario(AuthContext.User.Codigo);
                chamado.Usuario = usuario;
            }

            if (solucao.HasValue)
            {
                chamado.Solucao = new SolucaoService().FindById(solucao.Value);
                chamado.Status = ChamadoStatus.Fechado;

                ViewBag.MensagemSucesso = "Obrigado por utilizar o Smartdesk, sua responde ajudar outras pessoas a solucionar seus problemas mais rapidamente";
            }
            else
                ViewBag.MensagemSucesso = "Seu problema foi enfiado para a equipe de suporte que logo responderá";

            var chamadoService = new ChamadoService();
            chamadoService.Salvar(chamado);

            return RedirectToAction("Index");
        }

    }
}
