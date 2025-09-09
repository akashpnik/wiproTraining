using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureShop.Data;
using SecureShop.Models;
using Microsoft.AspNetCore.Authorization;

namespace SecureShop.Controllers
{
    public class ProductsController(ApplicationDbContext db) : Controller
    {
        private readonly ApplicationDbContext _db = db;

        // List products
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _db.Products.AsNoTracking().ToListAsync();
            return View(products);
        }

        // Product details + reviews
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _db.Products
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();
            return View(product);
        }

        // Create new product (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create() => View();

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // Edit product (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}
