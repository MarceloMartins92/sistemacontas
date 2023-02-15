namespace SistemaContas.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para retornar a consulta de contas
    /// </summary>
    public class ContasConsultaResultadoViewModel
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Valor { get; set; }
        public string? Data { get; set; }
        public string? Tipo { get; set; }
        public string? Categoria { get; set; }
    }
}
