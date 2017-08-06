using Capitulo1.Infraestrutura;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capitulo1.Areas.Seguranca.Controllers
{
    public class PapelAdminController : Controller
    {
        // GET: Seguranca/PapelAdmin cria visao de listagem dos papeis
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        private GerenciadorPapel RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorPapel>();
            }
        }
    }
}