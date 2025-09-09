using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureTaskApp.Data;
using System.Security.Claims;

namespace SecureTaskApp.Controllers
{
    [Authorize(Roles = "User")]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db) { _db = db; }

        [HttpGet("TaskList")]
        public async Task<IActionResult> TaskList()
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var tasks = await _db.Tasks.Where(t => t.OwnerUserId == uid)
                                       .OrderByDescending(t => t.CreatedAt)
                                       .ToListAsync();
            return View(tasks);
        }

        // Create a task (CSRF protected globally)
        [HttpPost("Create")]
        public async Task<IActionResult> Create(string title, string? description)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                TempData["Error"] = "Title is required.";
                return RedirectToAction("TaskList");
            }

            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            _db.Tasks.Add(new TaskItem { Title = title.Trim(), Description = description, OwnerUserId = uid });
            await _db.SaveChangesAsync();
            return RedirectToAction("TaskList");
        }

        // Edit only if owner AND has claim CanEditTask
        [Authorize(Policy = "CanEditTask")]
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(int id, string title, string? description)
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.OwnerUserId == uid);
            if (task == null) return Forbid();

            if (string.IsNullOrWhiteSpace(title))
            {
                TempData["Error"] = "Title is required.";
                return RedirectToAction("TaskList");
            }

            task.Title = title.Trim();
            task.Description = description;
            await _db.SaveChangesAsync();
            return RedirectToAction("TaskList");
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.OwnerUserId == uid);
            if (task == null) return Forbid();

            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
            return RedirectToAction("TaskList");
        }
    }
}
