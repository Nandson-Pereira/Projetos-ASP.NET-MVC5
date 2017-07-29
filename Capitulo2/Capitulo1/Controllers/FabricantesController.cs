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
    public class FabricantesController : Controller
    {
        EFContext context = new EFContext();


        //ACTION POST que recebe do modelo para edição e salvar a alteração..
        //Esta será responsável por receber os dados informados na visão no editar escolhido.
        //ACTION POST que  recebe os dados escolhidos pelo usuário para serem alterados.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricante fabricante)
        {
            if(ModelState.IsValid)
            {
                context.Entry(fabricante).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("index");
            }

            return View(fabricante);
        }


        //GET: Edit, acition responsavel pela visao do editar fabricantes.
        //ACTION GET de edição de dados no controlador. informar os dados a serem editados pelo id na visão.
        //action GET para ser gerada a visão de interação com o usuário.
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //erro caso o valor enviado seja nulo
            }

            Fabricante fabricante = context.Fabricantes.Find(id);

            if(fabricante == null)
            {
                return HttpNotFound(); //se o objeto nao for encontrado retorna um erro
            }

            return View(fabricante);
        }
        


        ////ACTION POST que recebe o modelo para inserção.
        ////Este será responsável por receber os dados informados na visão e pessistir.
        ////ACTION POST que  recebe os dados inseridos pelo usuário.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricante fabricante)
        {
            context.Fabricantes.Add(fabricante);
            context.SaveChanges();

            return RedirectToAction("index");
        }


        //GET: Criação da view de criar novo Fabricante
        //ACTION GET de inserção de dados no controlador. informar os dados na visão.
        //action GET para ser gerada a visão de interação com o usuário de inserção de novo fabricante.
        public ActionResult Create()
        {
            return View();
        }

        // GET: Fabricantes utilizada para criação da visao inicial index com a listagem de Fabricantes
        public ActionResult Index()
        {
            return View(context.Fabricantes.OrderBy(c => c.Nome));
        }
    }
}