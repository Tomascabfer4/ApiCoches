using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_COCHES.Modelos;

namespace API_COCHES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CochesController : ControllerBase
    {
        private readonly NeondbContext _context;

        public CochesController(NeondbContext context)
        {
            _context = context;
        }

        // GET: api/Coches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coche>>> GetCoches()
        {
            return await _context.Coches.ToListAsync();
        }

        // GET: api/Coches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coche>> GetCoche(int id)
        {
            var coche = await _context.Coches.FindAsync(id);

            if (coche == null)
            {
                return NotFound();
            }

            return coche;
        }

        // Filtrar por combustible
        [HttpGet("combustible/{tipo}")]
        public async Task<ActionResult<IEnumerable<Coche>>> GetCochesByCombustible(string tipo)
        {
            return await _context.Coches
                .Where(c => c.Combustible == tipo)
                .ToListAsync();
        }

        // Filtrar por marca
        [HttpGet("marca/{marca}")]
        public async Task<ActionResult<IEnumerable<Coche>>> GetCochesByMarca(string marca)
        {
            return await _context.Coches
                .Where(c => c.Marca == marca)
                .ToListAsync();
        }

        // Filtrar por kilometraje menor a X
        [HttpGet("kilometros/{maxKm}")]
        public async Task<ActionResult<IEnumerable<Coche>>> GetCochesByKilometros(int maxKm)
        {
            return await _context.Coches
                .Where(c => c.Kilometros <= maxKm)
                .ToListAsync();
        }

        // Filtrar por fecha más reciente
        [HttpGet("fecha/{fecha}")]
        public async Task<ActionResult<IEnumerable<Coche>>> GetCochesByFecha(DateOnly fecha)
        {
            return await _context.Coches
                .Where(c => c.Fecha >= fecha)
                .ToListAsync();
        }

        // Filtrar por coches disponibles
        [HttpGet("disponibles")]
        public async Task<ActionResult<IEnumerable<Coche>>> GetCochesDisponibles()
        {
            return await _context.Coches
                .Where(c => c.Disponible)
                .ToListAsync();
        }

        // Filtrar por matrícula
        [HttpGet("matricula/{matricula}")]
        public async Task<ActionResult<IEnumerable<Coche>>> GetCochesByMatricula(string matricula)
        {
            return await _context.Coches
                .Where(c => c.Matricula == matricula)
                .ToListAsync();
        }

        // Ordenar coches por precio (ascendente o descendente)
        [HttpGet("ordenar/precio/{order}")]
        public async Task<ActionResult<IEnumerable<Coche>>> GetCochesOrdenadosPorPrecio(string order)
        {
            var query = order.ToLower() == "asc"
                ? _context.Coches.OrderBy(c => c.Precio)
                : _context.Coches.OrderByDescending(c => c.Precio);

            return await query.ToListAsync();
        }

        // Ordenar coches por kilometraje
        [HttpGet("ordenar/kilometros/{order}")]
        public async Task<ActionResult<IEnumerable<Coche>>> GetCochesOrdenadosPorKilometros(string order)
        {
            var query = order.ToLower() == "asc"
                ? _context.Coches.OrderBy(c => c.Kilometros)
                : _context.Coches.OrderByDescending(c => c.Kilometros);

            return await query.ToListAsync();
        }

        // Ordenar coches por fecha de publicación
        [HttpGet("ordenar/fecha/{order}")]
        public async Task<ActionResult<IEnumerable<Coche>>> GetCochesOrdenadosPorFecha(string order)
        {
            var query = order.ToLower() == "asc"
                ? _context.Coches.OrderBy(c => c.Fecha)
                : _context.Coches.OrderByDescending(c => c.Fecha);

            return await query.ToListAsync();
        }

        // Ordenar coches por marca alfabéticamente
        [HttpGet("ordenar/marca/{order}")]
        public async Task<ActionResult<IEnumerable<Coche>>> GetCochesOrdenadosPorMarca(string order)
        {
            var query = order.ToLower() == "asc"
                ? _context.Coches.OrderBy(c => c.Marca)
                : _context.Coches.OrderByDescending(c => c.Marca);

            return await query.ToListAsync();
        }

        // PUT: api/Coches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoche(int id, Coche coche)
        {
            if (id != coche.Id)
            {
                return BadRequest();
            }

            _context.Entry(coche).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CocheExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Coches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coche>> PostCoche(Coche coche)
        {
            _context.Coches.Add(coche);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoche", new { id = coche.Id }, coche);
        }

        // DELETE: api/Coches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoche(int id)
        {
            var coche = await _context.Coches.FindAsync(id);
            if (coche == null)
            {
                return NotFound();
            }

            _context.Coches.Remove(coche);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CocheExists(int id)
        {
            return _context.Coches.Any(e => e.Id == id);
        }
    }
}
