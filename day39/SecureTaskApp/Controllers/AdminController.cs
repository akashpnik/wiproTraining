using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureTaskApp.Data;

namespace SecureTaskApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminController(ApplicationDbContext db) { _db = db; }

        [HttpGet("ManageTasks")]
        public async Task<IActionResult> ManageTasks()
        {
            var tasks = await _db.Tasks.OrderByDescending(t => t.CreatedAt).ToListAsync();
            return View(tasks);
        }
    }
}
