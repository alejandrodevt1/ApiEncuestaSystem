using System.ComponentModel.DataAnnotations;

namespace ApiEncuestaSystem.DTO
{
    public class EncuestaDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool IsDisponible { get; set; }
    }
}
