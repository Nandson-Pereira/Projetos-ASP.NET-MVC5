using System.Collections.Generic;

namespace Modelo.Cadastros
{
    public class Fabricante
    {
        public int? FabricanteId { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }

    }
}