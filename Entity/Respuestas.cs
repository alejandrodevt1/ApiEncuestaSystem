using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEncuestaSystem.Entity
{
    public class Respuestas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public string UsuarioId { get; set; }

        [Required(ErrorMessage = "El ID de la encuesta es obligatorio.")]
        public int EncuestaId { get; set; }

        [Required(ErrorMessage = "La pregunta es obligatoria.")]
        public int PreguntaID { get; set; }
        [ForeignKey("PreguntaID")]
        public virtual Preguntas Pregunta { get; set; }
        public int? OpcionId { get; set; }
        [ForeignKey("OpcionId")]
        public virtual Opciones Opciones { get; set; }
        public string RespuestaText { get; set; }

        public virtual UsuarioEncuesta UsuarioEncuestas { get; set; }
    }
}
