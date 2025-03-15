using ApiEncuestaSystem.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiEncuestaSystem.DTO
{
    public class CreacionOpcionesDTO
    {

        [Required(ErrorMessage = "El Id de la pregunta es obligatorio.")]
        public int PreguntaId { get; set; }

        [Required(ErrorMessage = "Las opciones son obligatorias.")]
        public string opciones { get; set; }
    }
}
