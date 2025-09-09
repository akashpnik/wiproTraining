using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureShop.Data;
using SecureShop.Models;
using System.Security.Claims;

namespace SecureShop.Controllers
{
    [Authorize]
    public class OrdersController(ApplicationDbContext db) : Controller
    {
        private readonly ApplicationDbContext _db = db;

        // Checkout page
        [HttpGet]
        public async Task<IActionResult> Checkout(int productId)
        {
            var product = await _db.Products.FindAsync(productId);
            if (product == null) return NotFound();

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Product = product,
                Quantity = 1,
                UnitPrice = product.Price
            };

            return View(orderItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderItem item)
        {
            var product = await _db.Products.FindAsync(item.ProductId);
            if (product == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var order = new Order
            {
                UserId = userId,
                Items =
                [
                    new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price
                    }
                ]
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(MyOrders));
        }

        // List user orders
        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var orders = await _db.Orders
                .Include(o => o.Items).ThenInclude(i => i.Product)
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return View(orders);
        }
    }
}
