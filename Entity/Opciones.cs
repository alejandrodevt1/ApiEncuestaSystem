using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEncuestaSystem.Entity
{
    public class Opciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Id de la pregunta es obligatorio.")]
        public int PreguntaId { get; set; }

        [ForeignKey("PreguntaId")]
        public virtual Preguntas Pregunta { get; set; }

        [Required(ErrorMessage = "Las opciones son obligatorias.")]
        public string opciones { get; set; }
    }
}
