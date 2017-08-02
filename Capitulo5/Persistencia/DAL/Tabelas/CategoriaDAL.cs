using Modelo.Cadastros;
using Modelo.Tabelas;
using Persistencia.Contexts;
using System.Data.Entity;
using System.Linq;

namespace Persistencia.DAL.Tabelas
{
    public class CategoriaDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Categoria>ObterCategoriasClassificadasPorNome()
        {
            return context.Categorias.OrderBy(b => b.Nome);
        }


        public Categoria ObterCategoriaPorId(int id)
        {
            return context.Categorias.Where(c => c.CategoriaId == id).Include("Produtos.Categoria").First();            
        }


        public void GravarCategoria(Categoria categoria)
        {
            if (categoria.CategoriaId == null)
            {
                context.Categorias.Add(categoria);
            }
            else
            {
                context.Entry(categoria).State = EntityState.Modified;
            }

            context.SaveChanges();
        }


        public Categoria EliminarCategoriaPorId(int id)
        {
            Categoria categoria = ObterCategoriaPorId(id);    
            context.Categorias.Remove(categoria);
            context.SaveChanges();

            return categoria;
        }
    }
}
