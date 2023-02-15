using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    public class ContasCadastroViewModel
    {
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome da conta.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o valor da conta.")]
        public decimal? Valor { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data da conta.")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Por favor, informe o tipo da conta.")]
        public int? Tipo { get; set; }

        [Required(ErrorMessage = "Por favor, informe a categoria da conta.")]
        public Guid? IdCategoria { get; set; }

        [Required(ErrorMessage = "Por favor, informe as observações da conta.")]
        public string? Observacoes { get; set; }

        /// <summary>
        /// Lista para exibir na páginas as opções de categorias
        /// </summary>
        public List<SelectListItem>? Categorias { get; set; }
    }
}
