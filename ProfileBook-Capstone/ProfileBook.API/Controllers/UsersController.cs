using Microsoft.AspNetCore.Mvc;

namespace ProfileBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            // Return sample data for testing connection
            var users = new[]
            {
                new { Id = 1, Name = "Akash", Email = "akash@example.com" },
                new { Id = 2, Name = "Mausam", Email = "mausam@example.com" },
                new { Id = 3, Name = "Aman", Email = "aman@example.com" }
            };
            
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = new { Id = id, Name = $"User {id}", Email = $"user{id}@example.com" };
            return Ok(user);
        }
    }
}
