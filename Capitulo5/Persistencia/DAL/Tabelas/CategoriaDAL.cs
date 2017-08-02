using Modelo.Tabelas;
using Persistencia.Contexts;
using System.Linq;

namespace Persistencia.DAL.Tabelas
{
    class CategoriaDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Categoria> ObterCategoriasClassificadasPorNome()
        {
            return context.Categorias.OrderBy(c => c.Nome);
        }
    }
}
