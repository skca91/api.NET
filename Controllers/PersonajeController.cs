using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.NET.Data;
using api.NET.Views;

namespace api.NET.Controllers
{
    [Route("api/characters")]
    [ApiController]
    public class PersonajeController : ControllerBase
    {
        private DisneyDbContext _context;

        public PersonajeController(DisneyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonajesView>>> GetPersonajes()
        {

            return await _context.Personajes.Select(p => new PersonajesView{ Nombre = p.Nombre, Imagen = p.Imagen} ).ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Personaje>> GetPersonaje(int id)
        {
            var personajes = await _context.Personajes.FindAsync(id);

            if (personajes == null)
            {
                return NotFound();
            }

            return personajes;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaje(int id, Personaje personaje)
        {
            if (id != personaje.IdPersonaje)
            {
                return BadRequest();
            }

            _context.Entry(personaje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonajesExists(id))
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
        public async Task<ActionResult<Personaje>> PostPersonaje(Personaje personaje)
        {
            _context.Personajes.Add(personaje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonajes", new { id = personaje.IdPersonaje }, personaje);
        }

   
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonaje(int id)
        {
            var personaje = await _context.Personajes.FindAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }

            _context.Personajes.Remove(personaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonajesExists(int id)
        {
            return _context.Personajes.Any(e => e.IdPersonaje == id);
        }
    }
}
