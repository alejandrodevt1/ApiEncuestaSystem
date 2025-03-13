using ApiEncuestaSystem.DTO;
using ApiEncuestaSystem.Entity;
using AutoMapper;

namespace ApiEncuestaSystem.Utils
{
    public class AutoMapperProfile:Profile
    {

        public AutoMapperProfile() {
            ConfigurarMapeoEncuesta();
            ConfigurarMapeoPreguntas();
        }

        private void ConfigurarMapeoEncuesta()
        {
            CreateMap<CreacionEncuestaDTO, Encuesta>();
            CreateMap<Encuesta,EncuestaDTO>();
        }

        private void ConfigurarMapeoPreguntas()
        {
            CreateMap<CreacionPreguntasDTO, Preguntas>();
            CreateMap<Preguntas, PreguntaDTO>();

        }
    }
}
