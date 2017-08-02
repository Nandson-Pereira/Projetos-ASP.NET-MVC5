using Modelo.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Modelo.Cadastros
{
    public class Produto
    {
        public int? ProdutoId { get; set; }
        public string Nome { get; set; }

        public int? CategoriaId { get; set; }
        public int? FabricanteId { get; set; }

        public Categoria Categoria { get; set; }
        public Fabricante Fabricante { get; set; }

    }
}