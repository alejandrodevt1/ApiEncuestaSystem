using ApiEncuestaSystem.DTO;
using ApiEncuestaSystem.Entity;
using ApiEncuestaSystem.Utils;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiEncuestaSystem.Controllers
{
    [Route("api/opciones")]
    [ApiController]
    public class OpcionController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public OpcionController(ApplicationDbContext context ,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}", Name = "ObtenerOpcionesPorId")]
        public async Task<ActionResult<OpcionesDTO>> Get(int id)
        {

            var opcionExiste = await context.Opciones.ProjectTo<OpcionesDTO>(
                mapper.ConfigurationProvider).FirstOrDefaultAsync(p => p.Id == id);

            if (opcionExiste is null) 
            {
                return NotFound(new { message = "La Opcion no fue encontrado en la base de datos." });
            }

            return opcionExiste;

        }

        [HttpGet("lista/{id:int}", Name = "ObtenerOpcionesPorPreguntaId")]
        public async Task<ActionResult<List<OpcionesDTO>>> GetOpcionesPorId(int id)
        {

            var preguntaExiste = await context.Preguntas.AnyAsync(p => p.Id == id);
            if (!preguntaExiste)
            {
                return NotFound(new { message = "La Pregunta no fue encontrado en la base de datos." });
            }

            return await context.Opciones.Where(o => o.PreguntaId == id).
                ProjectTo<OpcionesDTO>(mapper.ConfigurationProvider).ToListAsync();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registroBorrados = await context.Opciones.Where(o=> o.Id == id).
                ExecuteDeleteAsync();

            if(registroBorrados == 0)
            {
                return NotFound(new { message = "La Opcion no fue encontrado, por lo que no se pudo eliminar." });

            }

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CreacionOpcionesDTO creacionOpciones)
        {
            var opcionExiste = await context.Opciones.AnyAsync(o=> o.Id ==id);
            if (!opcionExiste)
            {
                return NotFound(new { message = "La Opcion no fue encontrada" });

            }

            var opcion = mapper.Map<Opciones>(creacionOpciones);
            opcion.Id = id;
            context.Update(opcion);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OpcionesRequestDTO opcionesRequest)
        {

            var pregunta = await context.Preguntas.FirstOrDefaultAsync(p => p.Id == opcionesRequest.PreguntaId);

            if (pregunta is null)
            {
                return NotFound(new { message = "La Pregunta no fue encontrado, por lo que no se pudo agregar opciones." });
            }

            if((int)pregunta.Tipo == 2)
            {
                return Conflict(new { message = "La Pregunta es de tipo Abierta, por lo que no se pueden agregar opciones." });

            }

            using var transacction = await context.Database.BeginTransactionAsync();
            try
            {

                List<Opciones> opciones = new List<Opciones>();

                foreach (var item in opcionesRequest.Opciones)
                {
                    var creacionOpcionesDTO = new CreacionOpcionesDTO
                    {
                        PreguntaId = pregunta.Id,
                        opciones = item.opciones,
                    };

                    var creacionOpciones = mapper.Map<Opciones>(creacionOpcionesDTO);
                    context.Add(creacionOpciones);
                    opciones.Add(creacionOpciones);
                }

                await context.SaveChangesAsync();
                await transacction.CommitAsync();
                return CreatedAtRoute("ObtenerOpcionesPorPreguntaId", new
                {
                    id = opcionesRequest.PreguntaId
                },
                opciones);

            }
            catch (Exception ex) 
            {
                await transacction.RollbackAsync();
                return StatusCode(500, new { message = $"Hubo un error al procesar las opciones: {ex.Message}" });
            }
        }





    }
}
