using ApiEncuestaSystem.DTO;
using ApiEncuestaSystem.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiEncuestaSystem.Controllers
{
    [Route("api/preguntas")]
    [ApiController]
    public class PreguntasController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PreguntasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("lista/{id:int}", Name = "ObtenerPreguntasPorEncuestaID")]
        public async Task<ActionResult<List<PreguntaDTO>>> Get(int id)
        {
            var existeEncuesta = await context.Encuesta.AnyAsync(e=> e.Id == id);
            if (!existeEncuesta)
            {
                return NotFound(new { message = "La Encuesta no fue encontrado en la base de datos." });

            }

            return await context.Preguntas.
                Where(e=>e.EncuestaId == id).ProjectTo<PreguntaDTO>(mapper.ConfigurationProvider).ToListAsync();
        }


        [HttpGet("{id:int}", Name = "ObtenerPreguntasPorId")]
        public async Task<ActionResult<PreguntaDTO>> GetPreguntaPorId(int id)
        {
            var pregunta = await context.Preguntas.ProjectTo<PreguntaDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p=> p.Id == id);

            if (pregunta is null)
            {
                return NotFound(new {message = "La pregunta no fue encontrado en la base de datos." });

            }

            return pregunta;

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CreacionPreguntasDTO creacionPreguntas)
        {
            var preguntaExiste = await context.Preguntas.AnyAsync(p=> p.Id == id);
            if (!preguntaExiste)
            {
                return NotFound(new { message = "La pregunta no fue encontrado en la base de datos." });

            }

            var pregunta = mapper.Map<Preguntas>(creacionPreguntas);
            pregunta.Id = id;
            context.Update(pregunta);
            await context.SaveChangesAsync();
            return NoContent();

        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registroBorrados = await context.Preguntas.Where(p=> p.Id == id).ExecuteDeleteAsync();

            if(registroBorrados == 0)
            {
                return NotFound(new { message = "La pregunta no fue encontrado en la base de datos, por lo que no se pudo eliminar." });

            }

            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PreguntasRequestDTO preguntasRequest)
        {
            using var transacction = await context.Database.BeginTransactionAsync();
            try
            {
                List<Preguntas> preguntasCreadas = new List<Preguntas>();

                foreach (var item in preguntasRequest.Preguntas)
                {
                    var creacionPreguntasDTO = new CreacionPreguntasDTO
                    {
                        EncuestaId = preguntasRequest.EncuestaId,
                        Pregunta = item.Pregunta,
                        Tipo = item.Tipo
                    };

                    var creacionPreguntas = mapper.Map<Preguntas>(creacionPreguntasDTO);
                    context.Add(creacionPreguntas);
                    preguntasCreadas.Add(creacionPreguntas);

                }

                await context.SaveChangesAsync();
                await transacction.CommitAsync();
                return CreatedAtRoute("ObtenerPreguntasPorEncuestaID", new
                {
                    id = preguntasRequest.EncuestaId
                }, preguntasCreadas);
            }
            catch (Exception ex) {
                await transacction.RollbackAsync();
                return StatusCode(500, new { message = $"Hubo un error al procesar las preguntas: {ex.Message}" });
            }

        }
    }
}
