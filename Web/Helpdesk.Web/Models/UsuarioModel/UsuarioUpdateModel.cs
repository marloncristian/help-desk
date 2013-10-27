using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Helpdesk.Web.Models.UsuarioModel
{
    public class UsuarioUpdateModel
    {
        public UsuarioUpdateModel()
        {
            TipoList = new List<SelectListItem>()
            {
                new SelectListItem(){
                    Value = "0",
                    Text = "Administrador"
                },
                new SelectListItem(){
                    Value = "1",
                    Text = "Comum"
                }
            };
        }

        [Required]
        public int Codigo { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public int Tipo { get; set; }

        public IEnumerable<SelectListItem> TipoList { get; set; }
    }
}