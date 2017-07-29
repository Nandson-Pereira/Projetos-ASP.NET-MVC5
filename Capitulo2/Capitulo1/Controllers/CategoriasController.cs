using Capitulo1.Contexts;
using Capitulo1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Capitulo1.Controllers
{
    public class CategoriasController : Controller
    {
        EFContext context = new EFContext();


        ////GET: Delete Categorias
        //public ActionResult Delete(int id)
        //{
        //    return View(categorias.Where(m => m.CategoriaId == id).First());
        //}



        ////ACTION POST que recebe o modelo para deletar a categoria de fato.
        ////Esta será responsável por deletar os dados informados na visão de deletar.
        ////ACTION POST que  recebe os dados escolhidos pelo usuário para serem deletados.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(Categoria categoria)
        //{
        //    categorias.Remove(categorias.Where(m => m.CategoriaId == categoria.CategoriaId).First());

        //    return RedirectToAction("Index");
        //}



        //GET: Detalhhes da Categorias
        //Comon ão haverá interação com o usuário na visão a ser gerada, implementaremos apenas a action HTTP GET
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = context.Categorias.Find(id);

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }


        //ACTION POST que recebe o modelo para edição e salvar a alteração..
        //Esta será responsável por receber os dados informados na visão no editar escolhido.
        //ACTION POST que  recebe os dados escolhidos pelo usuário para serem alterados.        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                context.Entry(categoria).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("index");
            }

            return View(categoria);

        }
        //GET: Edição da Categorias
        //ACTION GET de edição de dados no controlador. informar os dados a serem editados pelo id na visão.
        //action GET para ser gerada a visão de interação com o usuário.
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = context.Categorias.Find(id);

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }


        //ACTION POST que recebe o modelo para inserção.
        //Esta será responsável por receber os dados informados na visão.
        //ACTION POST que  recebe os dados inseridos pelo usuário.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            context.Categorias.Add(categoria);
            context.SaveChanges();

            return RedirectToAction("Index");
        }


        //GET: Criação da Categorias
        //ACTION GET de inserção de dados no controlador. informar os dados na visão.
        //action GET para ser gerada a visão de interação com o usuário.
        public ActionResult Create()
        {
            return View();
        }


        // GET: Categorias
        //ACTION principal onde é listada as categorias.
        public ActionResult Index()
        {
            return View(context.Categorias.OrderBy(c => c.Nome));
        }
    }
}