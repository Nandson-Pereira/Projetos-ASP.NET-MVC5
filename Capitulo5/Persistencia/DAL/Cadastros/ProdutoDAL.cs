﻿using Persistencia.Contexts;
using System.Linq;
using System.Data.Entity;
using Modelo.Cadastros;

namespace Persistencia.DAL.Cadastros
{
    class ProdutoDAL
    {
        private EFContext context = new EFContext();
        public IQueryable ObterProdutosClassificadosPorNome()
        {
            return context.Produtos.Include(c => c.Categoria).Include(f => f.Fabricante).OrderBy(n => n.Nome);
        }


        public Produto ObterProdutoPorId(int id)
        {
            return context.Produtos.Where(p => p.ProdutoId == id).Include(c => c.Categoria).Include(f => f.Fabricante).First();
        }


        public void GravarProduto(Produto produto)
        {
            if (produto.ProdutoId == null)
            {
                context.Produtos.Add(produto);
            }
            else
            {
                context.Entry(produto).State = EntityState.Modified;
            }

            context.SaveChanges();
        }


        public Produto EliminarProdutoPorId(int id)
        {
            Produto produto = ObterProdutoPorId(id);

            context.Produtos.Remove(produto);
            context.SaveChanges();

            return produto;
        }
    }
}
