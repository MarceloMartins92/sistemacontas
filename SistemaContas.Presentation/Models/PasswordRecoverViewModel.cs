using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    public class PasswordRecoverViewModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do usuário.")]
        public string? Email { get; set; }
    }
}
