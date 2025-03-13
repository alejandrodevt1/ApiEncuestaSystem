using ApiEncuestaSystem.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiEncuestaSystem.Entity
{
    public class Preguntas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Id de la encuesta es obligatorio.")]
        public int EncuestaId { get; set; }

        [JsonIgnore]
        [ForeignKey("EncuestaId")]
        public virtual Encuesta Encuesta { get; set; }

        [Required(ErrorMessage = "La pregunta es obligatoria.")]
        public string Pregunta { get; set; }
        [Required(ErrorMessage = "El tipo de pregunta es obligatorio.")]
        public TipoPregunta Tipo { get; set; }

    }
}
