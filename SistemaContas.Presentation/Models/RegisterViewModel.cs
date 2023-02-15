using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    public class RegisterViewModel
    {
        [RegularExpression("^[A-Za-zÀ-Üà-ü\\s]{8,150}$", ErrorMessage = "Por favor, informe um nome válido de 8 a 150 caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do usuário.")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do usuário.")]
        public string? Email { get; set; }

        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a senha do usuário.")]
        public string? Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem.")]
        [Required(ErrorMessage = "Por favor, confirme a senha do usuário.")]
        public string? SenhaConfirmacao { get; set; }
    }
}
