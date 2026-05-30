using ApiInteligenteTareas.Data;
using ApiInteligenteTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiInteligenteTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TareasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTareas(
            string? estado,
            string? prioridad,
            DateTime? fechaInicio,
            DateTime? fechaFin)
        {
            var tareas = _context.Tareas.AsQueryable();

            if (!string.IsNullOrEmpty(estado))
            {
                var estadosValidos = new[]
                {
                    "Pendiente",
                    "EnProceso",
                    "Completada"
                };

                if (!estadosValidos.Contains(estado))
                    return BadRequest("Estado inválido");

                tareas = tareas.Where(t => t.Estado == estado);
            }

            if (!string.IsNullOrEmpty(prioridad))
            {
                var prioridadesValidas = new[]
                {
                    "Baja",
                    "Media",
                    "Alta"
                };

                if (!prioridadesValidas.Contains(prioridad))
                    return BadRequest("Prioridad inválida");

                tareas = tareas.Where(t => t.Prioridad == prioridad);
            }

            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                if (fechaInicio > fechaFin)
                    return BadRequest("fechaInicio no puede ser mayor que fechaFin");

                tareas = tareas.Where(t =>
                    t.FechaVencimiento >= fechaInicio &&
                    t.FechaVencimiento <= fechaFin);
            }

            return await tareas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
                return NotFound();

            return tarea;
        }

        [HttpPost]
        public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTarea),
                new { id = tarea.Id },
                tarea);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, Tarea tarea)
        {
            if (id != tarea.Id)
                return BadRequest();

            _context.Entry(tarea).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
                return NotFound();

            _context.Tareas.Remove(tarea);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}