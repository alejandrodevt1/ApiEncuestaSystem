using ApiEncuestaSystem.DTO;
using ApiEncuestaSystem.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiEncuestaSystem.Controllers
{

    [Route("api/encuesta")]
    [ApiController]
    public class EncuestaController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EncuestaController(ApplicationDbContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreacionEncuestaDTO crearEncuestaDTO)
        {
            crearEncuestaDTO.IsDisponible = true;

            var encuesta = mapper.Map<Encuesta>(crearEncuestaDTO);
            context.Add(encuesta);
            await context.SaveChangesAsync();


            return CreatedAtRoute("ObtenerEncuestaId", new
            {
                id = encuesta.Id,
            },encuesta);

        } 

        [HttpGet("${id:int}",Name = "ObtenerEncuestaId")]
        public async Task<ActionResult<EncuestaDTO>> Get(int id)
        {
            var encuesta = await context.Encuesta.ProjectTo<EncuestaDTO>(
                mapper.ConfigurationProvider).FirstOrDefaultAsync(e=> e.Id == id);

            if (encuesta is null)
            {
                return NotFound(new { message = $"El {id} de la Encuesta no existe" });
            }

            return encuesta;
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CreacionEncuestaDTO
            crearEncuestaDTO)
        {
            var existeEncuesta = await context.Encuesta.AnyAsync(e=> e.Id == id);
            if (!existeEncuesta) 
            {
                return NotFound(new { message = "La encuesta no ha sido encontrada." });
            }

            var encuesta = mapper.Map<Encuesta>(crearEncuestaDTO);
            encuesta.Id = id;
            context.Update(encuesta);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("${id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registroBorrados = await context.Encuesta.Where(
                e => e.Id == id).ExecuteDeleteAsync();
            if (registroBorrados == 0)
            {
                return NotFound(new { message = $"No se encontro la encuesta con el {id} para eliminar." });
            }
            return NoContent();
        }
    }
}
