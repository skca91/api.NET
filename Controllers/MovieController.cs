using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.NET.Models;
using api.NET.Views;

namespace api.NET.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly DisneyDbContext _context;

        public MovieController(DisneyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            return await _context.Movie.Select(p => new MovieDTO { Title = p.Title, Image = p.Image, Creation = p.Creation }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movie.Include(c => c.Characters).Where(c => c.Id == id).FirstOrDefaultAsync();

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

        [HttpGet("{search}")]
        public async Task<IEnumerable<Movie>> Search(string? name, int? genre, string? order)
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
                //query = query.OrderBy<Movie, order>();
            }

            return await query.ToListAsync();
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
