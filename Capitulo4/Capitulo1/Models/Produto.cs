using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capitulo1.Models
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