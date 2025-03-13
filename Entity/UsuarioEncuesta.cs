using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEncuestaSystem.Entity
{
    public class UsuarioEncuesta
    {
        [Required(ErrorMessage = "El Id usuario es obligatorio.")]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual IdentityUser User { get; set; }

        [Required(ErrorMessage = "El Id de la encuesta es obligatorio.")]
        public int EncuestaID { get; set; }

        [ForeignKey("EncuestaID")]
        public virtual Encuesta Encuesta { get; set; }

    }
}
