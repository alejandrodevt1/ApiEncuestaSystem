using ApiEncuestaSystem.Entity;
using ApiEncuestaSystem.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiEncuestaSystem.DTO
{
    public class PreguntaDTO
    {
        public int Id { get; set; }
        public int EncuestaId { get; set; }
        public string Pregunta { get; set; }
        public TipoPregunta Tipo { get; set; }
    }
}
