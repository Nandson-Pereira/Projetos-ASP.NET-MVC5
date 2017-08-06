using System.ComponentModel.DataAnnotations;

namespace Capitulo1.Areas.Seguranca.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}