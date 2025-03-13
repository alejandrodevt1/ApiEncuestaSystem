using ApiEncuestaSystem.Utils;
using System.ComponentModel.DataAnnotations;

namespace ApiEncuestaSystem.DTO
{
    public class ListaPreguntasRequestDTO
    {
        [Required(ErrorMessage = "La pregunta es obligatoria.")]
        public string Pregunta { get; set; }
        [Required(ErrorMessage = "El tipo de pregunta es obligatorio.")]
        public TipoPregunta Tipo { get; set; }
    }
}
