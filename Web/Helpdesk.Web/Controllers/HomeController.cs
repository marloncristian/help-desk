using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpdesk.Dominio.Service.Security;
using Helpdesk.Infrastructure.Security;
using Helpdesk.Web.Filters;
using Helpdesk.Web.Models;

namespace Helpdesk.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authenticate]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            throw new ApplicationException("teste");
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Verifique os erros acima";
                return View();
            }

            var obj = new UsuarioService().GetUsuario(model.Login, model.Senha);
            if (obj == null)
            {
                ViewBag.Message = "Usuário ou Senha inválido";
                return View();
            }

            if (!AuthContext.SignIn(obj.Codigo, obj.Nome, obj.Tipo))
                RedirectToAction("LoginError");

            return RedirectToAction("Index");
        }
    }
}
