using Capitulo1.Areas.Seguranca.Models;
using Capitulo1.Infraestrutura;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Capitulo1.Areas.Seguranca.Controllers
{
    public class AdminController : Controller
    {


        // GET: DETAILS criar a visao para usuario
        [Authorize]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = GerenciadorUsuario.FindById(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }


        // POST: DELETE  action que receberá estes dados, que enfim realizará a persistência deles na base de dados
        [Authorize]
        [HttpPost]
        public ActionResult Delete(Usuario usuario)
        {
            Usuario user = GerenciadorUsuario.FindById(usuario.Id);

            if (user != null)
            {
                IdentityResult result = GerenciadorUsuario.Delete(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }


        // GET: DEELETE criar a visao para usuario Deletar
        [Authorize]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = GerenciadorUsuario.FindById(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }


        // POST: EDIT  action que receberá estes dados, que enfim realizará a persistência deles na base de dados
        [Authorize]
        [HttpPost]
        public ActionResult Edit(UsuarioViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = GerenciadorUsuario.FindById(usuarioViewModel.Id);

                usuario.UserName = usuarioViewModel.Nome;
                usuario.Email = usuarioViewModel.Email;
                usuario.PasswordHash = GerenciadorUsuario.PasswordHasher.HashPassword(usuarioViewModel.Senha);

                IdentityResult result = GerenciadorUsuario.Update(usuario);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            return View(usuarioViewModel);
        }


        // GET: EDIT criar a visao para usuario Editar
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                HttpStatusCode.BadRequest);
            }

            Usuario usuario = GerenciadorUsuario.FindById(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var usuarioViewModel = new UsuarioViewModel();

            usuarioViewModel.Id = usuario.Id;
            usuarioViewModel.Nome = usuario.UserName;
            usuarioViewModel.Email = usuario.Email;

            return View(usuarioViewModel);
        }


        // GET: CREATE criar a visao para usuario criar
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CREATE  action que receberá estes dados, que enfim realizará a persistência deles na base de dados
        [Authorize]
        [HttpPost]
        public ActionResult Create(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario user = new Usuario
                {
                    UserName = model.Nome,
                    Email = model.Email
                };

                IdentityResult result = GerenciadorUsuario.Create(user, model.Senha);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            return View(model);
        }


        //método específico (e privado) para o registro de possíveis erros ocorridos durante a tentativa de inserção de um usuário       
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        // GET: Seguranca/Admin
        [Authorize]
        public ActionResult Index()
        {
            return View(GerenciadorUsuario.Users);
        }

        
        private GerenciadorUsuario GerenciadorUsuario
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorUsuario>();
            }
        }
    }
}