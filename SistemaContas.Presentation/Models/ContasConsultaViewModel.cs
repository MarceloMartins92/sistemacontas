using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    public class ContasConsultaViewModel
    {
        /// <summary>
        /// Campo para capturar o filtro de data de inicio
        /// </summary>
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string? DataIni { get; set; }

        /// <summary>
        /// Campo para capturar o filtro de data de fim
        /// </summary>
        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string? DataFim { get; set; }

        /// <summary>
        /// Lista para exibir os registros dentro do grid da página
        /// </summary>
        public List<ContasConsultaResultadoViewModel>? Resultado { get; set; }
    }
}
