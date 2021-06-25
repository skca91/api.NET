using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.NET.Data;

namespace api.NET.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly DisneyDbContext _context;

        public PeliculasController(DisneyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peliculas>>> GetPeliculas()
        {
            return await _context.Peliculas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Peliculas>> GetPeliculas(int id)
        {
            var peliculas = await _context.Peliculas.FindAsync(id);

            if (peliculas == null)
            {
                return NotFound();
            }

            return peliculas;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeliculas(int id, Peliculas peliculas)
        {
            if (id != peliculas.IdPelicula)
            {
                return BadRequest();
            }

            _context.Entry(peliculas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculasExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Peliculas>> PostPeliculas(Peliculas peliculas)
        {
            _context.Peliculas.Add(peliculas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPeliculas", new { id = peliculas.IdPelicula }, peliculas);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeliculas(int id)
        {
            var peliculas = await _context.Peliculas.FindAsync(id);
            if (peliculas == null)
            {
                return NotFound();
            }

            _context.Peliculas.Remove(peliculas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeliculasExists(int id)
        {
            return _context.Peliculas.Any(e => e.IdPelicula == id);
        }
    }
}
