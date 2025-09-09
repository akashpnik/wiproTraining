using Microsoft.AspNetCore.Mvc;

public sealed class BooksController : Controller
{
    private readonly IBookRepository _books;
    private readonly IAuthorRepository _authors;
    private readonly IGenreRepository _genres;
    private readonly ILogger<BooksController> _log;

    public BooksController(IBookRepository b, IAuthorRepository a, IGenreRepository g, ILogger<BooksController> log)
    { _books=b; _authors=a; _genres=g; _log=log; }

    public IActionResult Index() => View(); // shell; AJAX will fill

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] BookQuery q)
    {
        try { return PartialView("_BookTable", await _books.GetPageAsync(q)); }
        catch (Exception ex) { _log.LogError(ex, "List failed"); return Problem("Failed to load books."); }
    }

    [HttpGet]
    public async Task<IActionResult> Form(int? id)
    {
        try
        {
            var authors = await _authors.GetAllAsync();
            var genres  = await _genres.GetAllAsync();
            Book? model = id is null ? new Book() : await _books.GetByIdAsync(id.Value);
            ViewBag.Authors = authors; ViewBag.Genres = genres;
            return PartialView("_BookForm", model ?? new Book());
        }
        catch (Exception ex) { _log.LogError(ex, "Form failed"); return Problem("Failed to load form."); }
    }

    [HttpPost]
    [IgnoreAntiforgeryToken] // simplify AJAX for demo
    public async Task<IActionResult> CreateAjax([FromBody] Book book)
    {
        if (!ModelState.IsValid) return BadRequest("Invalid data");
        try { var id = await _books.CreateAsync(book); return Json(new { ok=true, id }); }
        catch (Exception ex) { _log.LogError(ex, "Create failed"); return Json(new { ok=false, error="Create failed" }); }
    }

    [HttpPut]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> UpdateAjax([FromBody] Book book)
    {
        if (!ModelState.IsValid || book.Id<=0) return BadRequest("Invalid data");
        try { var ok = await _books.UpdateAsync(book); return Json(new { ok }); }
        catch (Exception ex) { _log.LogError(ex, "Update failed"); return Json(new { ok=false, error="Update failed" }); }
    }

    [HttpDelete]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> DeleteAjax(int id)
    {
        if (id<=0) return BadRequest("Invalid id");
        try { var ok = await _books.DeleteAsync(id); return Json(new { ok }); }
        catch (Exception ex) { _log.LogError(ex, "Delete failed"); return Json(new { ok=false, error="Delete failed" }); }
    }
}
