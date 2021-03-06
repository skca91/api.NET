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
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly DisneyDbContext _context;

        public MovieController(DisneyDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDetailDTO>> GetMovie(int id)
        {
            var movie = await _context.Movie.Where(m => m.Id == id).Select(p => new MovieDetailDTO { Title = p.Title, Image = p.Image, Creation = p.Creation }).FirstOrDefaultAsync();

            movie.characters = await _context.Character.Where(m => m.Movies.Contains(new Movie { Id = id })).Select(c => new CharacterDTO { Name = c.Name, Image = c.Image }).ToListAsync();

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var pelicula = await _context.Movie.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            _context.Movie.Remove(pelicula);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<IEnumerable<MovieDTO>> Search([FromQuery] string? name, [FromQuery] int? genre, [FromQuery] string? order)
        {
            IQueryable<Movie> query = _context.Movie;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Title.Contains(name));
            }

            if (genre != null)
            {
                query = query.Where(e => e.Genre.Id == genre);
            }

            if (!string.IsNullOrEmpty(order))
            {
                if (order == "ASC")
                {
                    query = query.OrderBy(p => p.Id);
                }
                else if (order == "DESC") {

                    query = query.OrderByDescending(p => p.Id);
                }
                
            }

            return await query.Select(p => new MovieDTO { Title = p.Title, Image = p.Image, Creation = p.Creation }).ToListAsync();
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
