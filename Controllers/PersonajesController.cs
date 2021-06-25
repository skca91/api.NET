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
    [Route("api/characters")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private DisneyDbContext _context;

        public PersonajesController(DisneyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personajes>>> GetPersonajes()
        {
            return await _context.Personajes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Personajes>> GetPersonajes(int id)
        {
            var personajes = await _context.Personajes.FindAsync(id);

            if (personajes == null)
            {
                return NotFound();
            }

            return personajes;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonajes(int id, Personajes personajes)
        {
            if (id != personajes.IdPersonaje)
            {
                return BadRequest();
            }

            _context.Entry(personajes).State = EntityState.Modified;

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
        public async Task<ActionResult<Personajes>> PostPersonajes(Personajes personajes)
        {
            _context.Personajes.Add(personajes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonajes", new { id = personajes.IdPersonaje }, personajes);
        }

   
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonajes(int id)
        {
            var personajes = await _context.Personajes.FindAsync(id);
            if (personajes == null)
            {
                return NotFound();
            }

            _context.Personajes.Remove(personajes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonajesExists(int id)
        {
            return _context.Personajes.Any(e => e.IdPersonaje == id);
        }
    }
}
