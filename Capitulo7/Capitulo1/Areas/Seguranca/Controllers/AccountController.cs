using Capitulo1.Areas.Seguranca.Models;
using Capitulo1.Infraestrutura;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Capitulo1.Areas.Seguranca.Controllers
{
    public class AccountController : Controller
    {

        //GET LOGIN primeira action que o argumento que chega é uma  string, de mesmo nome da variável da Querystring apontada
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }


        //POST LOGIN segunda action, que recebe também um objeto LoginViewModel , com os dados referentes ao usuário e senha informados.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel, string returnUrl)

        {
            if (ModelState.IsValid)
            {
                Usuario user = UserManager.Find(loginViewModel.Nome, loginViewModel.Senha);

                if (user == null)
                {
                    ModelState.AddModelError("", "Nome ou Senha Inválido(s).");
                }
                else
                {
                    ClaimsIdentity ident = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);

                    if (returnUrl == null)
                        returnUrl = "/Home";

                    return Redirect(returnUrl);
                }
            }
            return View(loginViewModel);
        }


       // duas propriedades de leitura.Estas foram criadas apenas para facilitar a invocação dos métodos usados pelas actions acima .
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private GerenciadorUsuario UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorUsuario>();
            }
        }
    }
}