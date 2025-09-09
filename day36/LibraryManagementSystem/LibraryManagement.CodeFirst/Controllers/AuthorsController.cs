using LibraryManagement.CodeFirst.Models;
using LibraryManagement.CodeFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.CodeFirst.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            try
            {
                var authors = await _authorService.GetAllAuthorsAsync();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);
                if (author == null)
                    return NotFound($"Author with ID {id} not found.");

                return Ok(author);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            try
            {
                var createdAuthor = await _authorService.CreateAuthorAsync(author);
                return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.AuthorID }, createdAuthor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            try
            {
                var updatedAuthor = await _authorService.UpdateAuthorAsync(id, author);
                if (updatedAuthor == null)
                    return NotFound($"Author with ID {id} not found.");

                return Ok(updatedAuthor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var result = await _authorService.DeleteAuthorAsync(id);
                if (!result)
                    return NotFound($"Author with ID {id} not found.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
