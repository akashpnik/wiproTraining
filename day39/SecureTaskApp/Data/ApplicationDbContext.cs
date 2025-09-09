using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SecureTaskApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Extra profile fields go here if needed
    }

    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        // Razor encodes this by default in views to mitigate XSS
        public string? Description { get; set; }

        public string OwnerUserId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();
    }

    public static class DefaultSeed
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Roles
            foreach (var role in new[] { "Admin", "User" })
            {
                if (!await roleMgr.RoleExistsAsync(role))
                    await roleMgr.CreateAsync(new IdentityRole(role));
            }

            // Admin user
            const string adminEmail = "admin@example.com";
            var admin = await userMgr.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                await userMgr.CreateAsync(admin, "Admin@12345"); // dev only; change in real life
                await userMgr.AddToRoleAsync(admin, "Admin");
            }

            // Normal user with CanEditTask claim
            const string userEmail = "user@example.com";
            var user = await userMgr.FindByEmailAsync(userEmail);
            if (user == null)
            {
                user = new ApplicationUser { UserName = userEmail, Email = userEmail, EmailConfirmed = true };
                await userMgr.CreateAsync(user, "User@12345"); // dev only; change in real life
                await userMgr.AddToRoleAsync(user, "User");
                await userMgr.AddClaimAsync(user, new System.Security.Claims.Claim("permission", "CanEditTask"));
            }
        }
    }
}
