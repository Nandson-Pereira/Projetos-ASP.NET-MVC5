using System.Web.Mvc;
using System.Net;
using Modelo.Cadastros;
using Servico.Cadastros;
using Servico.Tabelas;

namespace Capitulo1.Controllers
{
    public class ProdutosController : Controller
    {
        //Declaração dos serviços dsponiveis para Produto

        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();


        //buscando eliminar a redudancia (mesmos codigos para obter o objeto) no codigo das actions details, Edit e Delete da 
        //exibição do produto, criaremos um metodo privado ObterVisaoProdutoPorId(int? id) chamada ao serviço que retornará 
        //o produto solicitado pelo usuário para exibição ja que esse codigo repetia-se nos GETS de details, Edit e Delete.

        private ActionResult ObterVisaoProdutoPorId(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = produtoServico.ObterProdutoPorId((int)id);

            if (produto == null)
            {
                return HttpNotFound();
            }

            return View(produto);
        }


        // na action GET EDIT e CREATE existe o fato de passar as categorias e fabricantes para a ViewBag,que popularão 
        // os DropDownLists da visão.Sendo assim, vamos criar um método privado que resolverá este problema.
        // na assinatura do método, o parâmetro produto é opcional.E quando ele não existir, é atribuído null a ele.Isso foi
        // adotado para podermos diferenciar quando o quarto parâmetro doSelectList() será informado.

        private void PopularViewBag(Produto produto = null)
        {
            if (produto == null)
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome");
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome");
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome", produto.FabricanteId);
            }
        }



        // Com a implementação das action GET , nos restam agora as que respondem ao HTTP POST.Duas delas referem-se à atualização ou
        // inserção de um produto. Eelas são semelhantes. Desta maneira, vamos criar um método privado para estas requisições.
        // implementação  há a invocação ao método de serviço para gravar o produto, independente de ser atualização ou inserção
        private ActionResult GravarProduto(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produtoServico.GravarProduto(produto);
                    return RedirectToAction("Index");
                }
                return View(produto);
            }
            catch
            {
                return View(produto);
            }
        }


        // GET: Produtos
        //Action para a visao index dos produtos
        public ActionResult Index()
        {
            return View(produtoServico.ObterProdutosClassificadosPorNome());
        }



        // GET: Produtos/Details/5 rederriza a visao de detalhes.
        public ActionResult Details(int? id)
        {
            return ObterVisaoProdutoPorId(id);
        }


        // GET: Produtos/Create
        public ActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        // POST: Produtos/Create
        //Create para o POST , que persistirá os dados informados na visão
        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            return GravarProduto(produto);
        }



        // GET: Produtos/Edit/cria a vier edite para interação do usuario
        public ActionResult Edit(int? id)
        {
            PopularViewBag(produtoServico.ObterProdutoPorId((int)id));
            return ObterVisaoProdutoPorId(id);
        }



        // POST: Produtos/Edit/action Edit ( POST ) para persistir suas alterações.
        [HttpPost]
        public ActionResult Edit(Produto produto)
        {
            return GravarProduto(produto);
        }



        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            return ObterVisaoProdutoPorId(id);
        }



        // POST: Produtos/Delete/5
        // POST: Produtos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Produto produto = produtoServico.EliminarProdutoPorId(id);
                TempData["Message"] = "Produto " + produto.Nome.ToUpper() + " foi removido";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
