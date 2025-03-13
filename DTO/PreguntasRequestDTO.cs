using System.ComponentModel.DataAnnotations;

namespace ApiEncuestaSystem.DTO
{
    public class PreguntasRequestDTO
    {

        [Required(ErrorMessage = "El Id de la encuesta es obligatorio.")]
        public int EncuestaId { get; set; }

        [Required(ErrorMessage = "Lista de preguntas es obligatorio.")]
        [MinLength(1, ErrorMessage = "Debe haber al menos una pregunta.")]
        public List<ListaPreguntasRequestDTO> Preguntas { get; set; }
    }
}
