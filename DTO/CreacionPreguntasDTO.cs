using ApiEncuestaSystem.Entity;
using ApiEncuestaSystem.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiEncuestaSystem.DTO
{
    public class CreacionPreguntasDTO
    {
        [Required(ErrorMessage = "El Id de la encuesta es obligatorio.")]
        public int EncuestaId { get; set; }

        [Required(ErrorMessage = "La pregunta es obligatoria.")]
        public string Pregunta { get; set; }
        [Required(ErrorMessage = "El tipo de pregunta es obligatorio.")]
        public TipoPregunta Tipo { get; set; }
    }
}
