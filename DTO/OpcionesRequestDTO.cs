using System.ComponentModel.DataAnnotations;

namespace ApiEncuestaSystem.DTO
{
    public class OpcionesRequestDTO
    {

        [Required(ErrorMessage = "El Id de la pregunta es obligatorio.")]
        public int PreguntaId { get; set; }

        [Required(ErrorMessage = "Lista de opciones es obligatorio.")]
        [MinLength(2, ErrorMessage = "Debe haber al menos dos opciones.")]
        public List<ListaOpcionesRequestDTO> Opciones { get; set; }
    }
}
