using Modelo.Cadastros;
using Persistencia.Contexts;
using System.Linq;

namespace Persistencia.DAL.Cadastros
{
    class FabrincanteDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Fabricante> ObterFabricantesClassificadosPorNome()
        {
            return context.Fabricantes.OrderBy(f => f.Nome);
        }
    }
}
