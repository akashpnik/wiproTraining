using LibraryManagement.CodeFirst.Models;
using LibraryManagement.CodeFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.CodeFirst.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            try
            {
                var genres = await _genreService.GetAllGenresAsync();
                return Ok(genres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            try
            {
                var genre = await _genreService.GetGenreByIdAsync(id);
                if (genre == null)
                    return NotFound($"Genre with ID {id} not found.");

                return Ok(genre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Genre>> CreateGenre(Genre genre)
        {
            try
            {
                var createdGenre = await _genreService.CreateGenreAsync(genre);
                return CreatedAtAction(nameof(GetGenre), new { id = createdGenre.GenreID }, createdGenre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, Genre genre)
        {
            try
            {
                var updatedGenre = await _genreService.UpdateGenreAsync(id, genre);
                if (updatedGenre == null)
                    return NotFound($"Genre with ID {id} not found.");

                return Ok(updatedGenre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                var result = await _genreService.DeleteGenreAsync(id);
                if (!result)
                    return NotFound($"Genre with ID {id} not found.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
