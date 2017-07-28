using Capitulo1.Contexts;
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

        // GET: Fabricantes utilizada para criação da visao inicial index com a listagem de Fabricantes
        public ActionResult Index()
        {
            return View(context.Fabricantes.OrderBy(c => c.Nome));
        }
    }
}