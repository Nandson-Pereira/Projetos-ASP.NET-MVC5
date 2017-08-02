using Modelo.Cadastros;
using Servico.Cadastros;
using Servico.Tabelas;
using System.Net;
using System.Web.Mvc;

namespace Capitulo1.Controllers
{
    public class FabricantesController : Controller
    {
        //Declaração dos serviços dsponiveis para Produto

        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();



        //buscando eliminar a redudancia (mesmos codigos para obter o objeto) no codigo das actions details, Edit e Delete da 
        //exibição do produto, criaremos um metodo privado ObterVisaoProdutoPorId(int? id) chamada ao serviço que retornará 
        //o produto solicitado pelo usuário para exibição ja que esse codigo repetia-se nos GETS de details, Edit e Delete.

        private ActionResult ObterVisaoFabricantePorId(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fabricante fabricante = fabricanteServico.ObterFabricantePorId((int)id);

            if (fabricante == null)
            {
                return HttpNotFound();
            }

            return View(fabricante);
        }



        // Com a implementação das action GET , nos restam agora as que respondem ao HTTP POST.Duas delas referem-se à atualização ou
        // inserção de um produto. Eelas são semelhantes. Desta maneira, vamos criar um método privado para estas requisições.
        // implementação  há a invocação ao método de serviço para gravar o produto, independente de ser atualização ou inserção
        private ActionResult GravarFabricante(Fabricante fabricante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    fabricanteServico.GravarFabricante(fabricante);
                    return RedirectToAction("Index");
                }
                return View(fabricante);
            }
            catch
            {
                return View(fabricante);
            }
        }

        // POST: Fabricantes/Delete
        ////Esta será responsável por deletar os dados informados na visão de deletar.
        ////ACTION POST que  recebe os dados escolhidos pelo usuário para serem deletados.
        [HttpPost]        
        public ActionResult Delete(int id)
        {
            try
            {
                Fabricante fabricante = fabricanteServico.EliminarFabricantePorId(id);
                //criamos um valor associado à chave [Message].Na visão, será possível recuperar este valor
                TempData["Message"] = "Fabricante " + fabricante.Nome.ToUpper() + " foi removido!!!!";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //GET: Delete Fabricante
        //ACITION destinada a criação da visao pra escolha do item a ser deletado.
        public ActionResult Delete(int? id)
        {
            return ObterVisaoFabricantePorId(id);
        }


        //GET: Detalhhes da Fabricantes
        //Como não haverá interação com o usuário na visão a ser gerada, implementaremos apenas a action HTTP GET
        public ActionResult Details(int? id)
        {
            return ObterVisaoFabricantePorId(id);
        }


        //ACTION POST que recebe do modelo para edição e salvar a alteração..
        //Esta será responsável por receber os dados informados na visão no editar escolhido.
        //ACTION POST que  recebe os dados escolhidos pelo usuário para serem alterados.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricante fabricante)
        {
            return GravarFabricante(fabricante);
        }


        //GET: Edit, acition responsavel pela visao do editar fabricantes.
        //ACTION GET de edição de dados no controlador. informar os dados a serem editados pelo id na visão.
        //action GET para ser gerada a visão de interação com o usuário.
        public ActionResult Edit(int? id)
        {
            return ObterVisaoFabricantePorId(id);
        }



        ////ACTION POST que recebe o modelo para inserção.
        ////Este será responsável por receber os dados informados na visão e pessistir.
        ////ACTION POST que  recebe os dados inseridos pelo usuário.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricante fabricante)
        {
            return GravarFabricante(fabricante);
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
            return View(fabricanteServico.ObterFabricantesClassificadosPorNome());
        }
    }
}