using Helpdesk.Dominio.Core.Security;
using Helpdesk.Dominio.Service.Security;
using Helpdesk.Web.Filters;
using Helpdesk.Web.Models.UsuarioModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Helpdesk.Web.Controllers
{
    public class UsuarioController : Controller
    {
        [Authenticate]
        public ActionResult Index()
        {
            var usuarios = new UsuarioService().GetUsuarios();
            var model = new UsuarioIndexModel()
            {
                Usuarios = usuarios.Select(o => new UsuarioModel()
                {
                    Codigo = o.Codigo,
                    Nome = o.Nome,
                    Login = o.Login
                }).ToList()
            };
            return View(model);
        }

        public ActionResult Update()
        {
            return View(new UsuarioUpdateModel());
        }

        [HttpPost]
        public ActionResult Update(UsuarioUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MensagemErro = "Não foi possível salvar o usuário";
                return View(model);
            }

            var service = new UsuarioService();

            var obj = service.GetUsuario(model.Login);
            if (obj != null)
            {
                ViewBag.MensagemErro = "Já existe um usuário associado a este login";
                return View(model);
            }

            var usuario = new Usuario()
            {
                Codigo = model.Codigo,
                Nome = model.Nome,
                Login = model.Login,
                Senha = model.Senha,
                Tipo = (UsuarioTipo)model.Tipo
            };
            service.Save(usuario);

            ViewBag.MensagemSucesso = "Usuario salvo com sucesso";
            return View(model);
        }

        public ActionResult Delete(int codigo)
        {
            try
            {
                new UsuarioService().RemoveUsuario(codigo);
                ViewBag.MensagemSucesso = "Usuário Excluido com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.MensagemErro = "Não foi possível excluir o usuário";
                return View();
            }
        }
    }
}
