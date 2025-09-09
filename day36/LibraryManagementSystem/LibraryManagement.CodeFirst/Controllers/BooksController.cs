using LibraryManagement.CodeFirst.Models;
using LibraryManagement.CodeFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.CodeFirst.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                    return NotFound($"Book with ID {id} not found.");

                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            try
            {
                var createdBook = await _bookService.CreateBookAsync(book);
                return CreatedAtAction(nameof(GetBook), new { id = createdBook.BookID }, createdBook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            try
            {
                var updatedBook = await _bookService.UpdateBookAsync(id, book);
                if (updatedBook == null)
                    return NotFound($"Book with ID {id} not found.");

                return Ok(updatedBook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var result = await _bookService.DeleteBookAsync(id);
                if (!result)
                    return NotFound($"Book with ID {id} not found.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("with-details")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksWithDetails()
        {
            try
            {
                var books = await _bookService.GetBooksWithAuthorsAndGenresAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
