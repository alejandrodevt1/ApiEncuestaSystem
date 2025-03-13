using System.ComponentModel.DataAnnotations;

namespace ApiEncuestaSystem.DTO
{
    public class CreedencialesUsuarioDTO
    {
        [EmailAddress(ErrorMessage = "Email Invalido.")]
        [Required(ErrorMessage = "El Email es obligatorio.")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "El Password es obligatorio")]
        [MinLength(4, ErrorMessage = "La contraseña no puede ser menor de 4 caracteres.")]
        public required string Password { get; set; }
    }
}
