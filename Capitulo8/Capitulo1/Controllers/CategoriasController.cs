using Modelo.Tabelas;
using Servico.Cadastros;
using Servico.Tabelas;
using System.Net;
using System.Web.Mvc;

namespace Capitulo1.Controllers
{
    public class CategoriasController : Controller
    {

        //Declaração dos serviços dsponiveis para Produto

        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();



        //buscando eliminar a redudancia (mesmos codigos para obter o objeto) no codigo das actions details, Edit e Delete da 
        //exibição do produto, criaremos um metodo privado ObterVisaoProdutoPorId(int? id) chamada ao serviço que retornará 
        //o produto solicitado pelo usuário para exibição ja que esse codigo repetia-se nos GETS de details, Edit e Delete.

        private ActionResult ObterVisaoCategoriaPorId(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = categoriaServico.ObterCategoriaPorId((int)id);

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }



        // Com a implementação das action GET , nos restam agora as que respondem ao HTTP POST.Duas delas referem-se à atualização ou
        // inserção de um produto. Eelas são semelhantes. Desta maneira, vamos criar um método privado para estas requisições.
        // implementação  há a invocação ao método de serviço para gravar o produto, independente de ser atualização ou inserção
        private ActionResult GravarCategoria(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoriaServico.GravarCategoria(categoria);
                    return RedirectToAction("Index");
                }
                return View(categoria);
            }
            catch
            {
                return View(categoria);
            }
        }


        // POST: Fabricantes/Delete
        ////Esta será responsável por deletar os dados informados na visão de deletar.
        ////ACTION POST que  recebe os dados escolhidos pelo usuário para serem deletados.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Categoria categoria = categoriaServico.EliminarCategoriaPorId(id);
                //criamos um valor associado à chave [Message].Na visão, será possível recuperar este valor
                TempData["Message"] = "Categoria " + categoria.Nome.ToUpper() + " foi removida!!!!";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }


        //GET: Delete Categorias
        //action com a finalidade de criar uma visao para escolha do objeto a ser deletado do DB
        public ActionResult Delete(int? id)
        {
            return ObterVisaoCategoriaPorId(id);
        }


        //GET: Detalhhes da Categorias
        //Comon ão haverá interação com o usuário na visão a ser gerada, implementaremos apenas a action HTTP GET
        public ActionResult Details(int? id)
        {
            return ObterVisaoCategoriaPorId(id);
        }


        //ACTION POST que recebe o modelo para edição e salvar a alteração..
        //Esta será responsável por receber os dados informados na visão no editar escolhido.
        //ACTION POST que  recebe os dados escolhidos pelo usuário para serem alterados.        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            return GravarCategoria(categoria);
        }
        //GET: Edição da Categorias
        //ACTION GET de edição de dados no controlador. informar os dados a serem editados pelo id na visão.
        //action GET para ser gerada a visão de interação com o usuário.
        public ActionResult Edit(int? id)
        {
            return ObterVisaoCategoriaPorId(id);
        }


        //ACTION POST que recebe o modelo para inserção.
        //Esta será responsável por receber os dados informados na visão.
        //ACTION POST que  recebe os dados inseridos pelo usuário.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            return GravarCategoria(categoria);
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
            return View(categoriaServico.ObterCategoriasClassificadasPorNome());
        }
    }
}