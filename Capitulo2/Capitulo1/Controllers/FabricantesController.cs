using Capitulo1.Contexts;
using Capitulo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capitulo1.Controllers
{
    public class FabricantesController : Controller
    {
        EFContext context = new EFContext();



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