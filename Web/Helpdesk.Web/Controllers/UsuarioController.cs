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

        [Authenticate]
        public ActionResult Update(int codigo = 0)
        {
            if (codigo == 0)
                return View(new UsuarioUpdateModel());

            var service = new UsuarioService();
            var usuario = service.GetUsuario(codigo);
            return View(new UsuarioUpdateModel()
            {
                Codigo = usuario.Codigo,
                Login = usuario.Login,
                Nome = usuario.Nome,
                Tipo = usuario.Tipo.GetHashCode()
            });
        }

        [HttpPost]
        [Authenticate]
        public ActionResult Update(UsuarioUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MensagemErro = "Não foi possível salvar o usuário";
                return View(model);
            }

            var service = new UsuarioService();
            if (model.Codigo == 0)
            {
                var obj = service.GetUsuario(model.Login);
                if (obj != null)
                {
                    ViewBag.MensagemErro = "Já existe um usuário associado a este login";
                    return View(model);
                }
            }

            var usuario = (model.Codigo != 0) 
                ? service.GetUsuario(model.Codigo) 
                : new Usuario();
            usuario.Nome = model.Nome;
            usuario.Login = model.Login;
            usuario.Senha = model.Senha;
            usuario.Tipo = (UsuarioTipo)model.Tipo;
            service.Save(usuario);

            ViewBag.MensagemSucesso = "Usuario salvo com sucesso";
            return View(model);
        }

        [Authenticate]
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
