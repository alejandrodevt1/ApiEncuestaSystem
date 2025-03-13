using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiEncuestaSystem.Entity
{
    public class Encuesta
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID Usuario es obligatorio")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public virtual IdentityUser User { get; set; }

        [Required(ErrorMessage = "El Titulo es obligatorio")]
        [StringLength(200, ErrorMessage = "El título no puede tener más de 150 caracteres.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "La Descripcion es obligatorio")]
        [StringLength(200, ErrorMessage = "La Descripcion no puede tener más de 200 caracteres.")]

        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La Disponibilidad de la encuesta es obligatorio")]
        public bool IsDisponible { get; set; }


    }
}
