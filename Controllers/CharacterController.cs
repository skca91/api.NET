using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.NET.Models;
using api.NET.Views;
using Microsoft.AspNetCore.Authorization;

namespace api.NET.Controllers
{
    [Authorize]
    [Route("api/characters")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private DisneyDbContext _context;

        public CharacterController(DisneyDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDetailDTO>> GetCharacter(int id)
        {
            var character = await _context.Character.Where(c => c.Id == id).Select(p => new CharacterDetailDTO { Name = p.Name, Image = p.Image, Age = p.Age, Weight = p.Weight, Story = p.Story }).FirstOrDefaultAsync();

            character.movies = await _context.Movie.Where(m => m.Characters.Contains(new Character { Id = id})).Select(m => new MovieDTO { Title = m.Title, Image = m.Image, Creation = m.Creation}).ToListAsync();

            if (character == null)
            {
                return NotFound();
            }

            return character;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, Character character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }

            _context.Entry(character).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
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
        public async Task<ActionResult<Character>> PostCharacter(Character character)
        {
            _context.Character.Add(character);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacter", new { id = character.Id }, character);
        }

   
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var personaje = await _context.Character.FindAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }

            _context.Character.Remove(personaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<IEnumerable<CharacterDTO>> Search([FromQuery]string name, [FromQuery] int? age, [FromQuery] int? movies)
        {
            IQueryable<Character> query = _context.Character;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            if (age != null)
            {
                query = query.Where(e => e.Age == age);
            }

            if (movies != null)
            {

                query = query.Where(e => e.Movies.Contains(new Movie { Id = movies.Value }));
            }

            return await query.Select(p => new CharacterDTO { Name = p.Name, Image = p.Image }).ToListAsync();
        }

        private bool CharacterExists(int id)
        {
            return _context.Character.Any(e => e.Id == id);
        }
    }
}
