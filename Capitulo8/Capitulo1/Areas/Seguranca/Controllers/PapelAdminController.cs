
using Capitulo1.Areas.Seguranca.Models;
using Capitulo1.Infraestrutura;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Capitulo1.Areas.Seguranca.Controllers
{
    public class PapelAdminController : Controller
    {

        //POST EDIT
        [HttpPost]
        public ActionResult Edit(PapelModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsParaAdicionar ??
                new string[] { })
                {
                    result = UserManager.AddToRole(userId, model.NomePapel);

                    if (!result.Succeeded)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }

                foreach (string userId in model.IdsParaRemover ??
                new string[] { })
                {
                    result = UserManager.RemoveFromRole(userId, model.NomePapel);

                    if (!result.Succeeded)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }

                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }


        //GET EDIT
        public ActionResult Edit(string id)
        {
            Papel papel = RoleManager.FindById(id);
            string[] memberIDs = papel.Users.Select(x => x.UserId).ToArray();
            IEnumerable<Usuario> membros = UserManager.Users.Where(x => memberIDs.Any(y => y == x.Id));
            IEnumerable<Usuario> naoMembros = UserManager.Users.Except(membros);

            return View(new PapelEditModel
            {
                Papel = papel,
                Membros = membros,
                NaoMembros = naoMembros
            });
        }

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