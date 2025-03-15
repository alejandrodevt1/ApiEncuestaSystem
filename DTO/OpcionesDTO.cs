using ApiEncuestaSystem.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiEncuestaSystem.DTO
{
    public class OpcionesDTO
    {
        public int Id { get; set; }
        public int PreguntaId { get; set; }
        public string opciones { get; set; }
    }
}
