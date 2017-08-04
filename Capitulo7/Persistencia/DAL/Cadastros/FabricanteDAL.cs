using Modelo.Cadastros;
using Persistencia.Contexts;
using System.Data.Entity;
using System.Linq;


namespace Persistencia.DAL.Cadastros
{
    public class FabricanteDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Fabricante>ObterFabricantesClassificadosPorNome()
        {
            return context.Fabricantes.OrderBy(b => b.Nome);
        }


        public Fabricante ObterFabricantePorId(int id)
        {
            return context.Fabricantes.Where(f => f.FabricanteId == id).Include("Produtos.Categoria").First();
        }


        public void GravarFabricante(Fabricante fabricante)
        {
            if (fabricante.FabricanteId == null)
            {
                context.Fabricantes.Add(fabricante);
            }
            else
            {
                context.Entry(fabricante).State = EntityState.Modified;
            }

            context.SaveChanges();
        }


        public Fabricante EliminarFabricantePorId(int id)
        {
            Fabricante fabricante = ObterFabricantePorId(id);
            context.Fabricantes.Remove(fabricante);
            context.SaveChanges();

            return fabricante;
        }
    }
}