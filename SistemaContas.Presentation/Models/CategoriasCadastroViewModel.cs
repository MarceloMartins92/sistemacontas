using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    public class CategoriasCadastroViewModel
    {
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome da categoria.")]
        public string? Nome { get; set; }
    }
}
