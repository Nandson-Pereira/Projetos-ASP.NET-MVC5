using Modelo.Tabelas;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Cadastros
{
    public class Produto
    {
        [DisplayName("Id")]
        public int? ProdutoId { get; set; }

        [StringLength(100, ErrorMessage = " O nome do PRODUTO precisa ter no mínimo 10 caracteres! ", MinimumLength = 10)]
        [Required(ErrorMessage = "Informe o nome do produto!")]
        public string Nome { get; set; }

        [DisplayName("Data de Cadastro")]
        [Required(ErrorMessage = "Informe a data de cadastro do produto!")]
        public DateTime? DataCadastro { get; set; }


        [DisplayName("Categoria")]
        public int? CategoriaId { get; set; }

        [DisplayName("Fabricante")]
        public int? FabricanteId { get; set; }


        public Categoria Categoria { get; set; }
        public Fabricante Fabricante { get; set; }

    }
}