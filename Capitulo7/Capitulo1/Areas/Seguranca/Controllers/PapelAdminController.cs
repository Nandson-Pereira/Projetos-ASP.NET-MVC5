using Capitulo1.Areas.Seguranca.Models;
using Capitulo1.Infraestrutura;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        //GET: Create Papeis/Roles
        public ActionResult Create()
        {
            return View();
        }


        //POST: Create Papeis/Roles
        [HttpPost]
        public ActionResult Create([Required]string nome)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = RoleManager.Create(new Papel(nome));

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            return View(nome);
        }



        //metodos de obter configuração e metodos auxiliares as actions
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private GerenciadorUsuario UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorUsuario>();
            }
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