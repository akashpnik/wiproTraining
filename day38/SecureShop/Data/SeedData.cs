using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecureShop.Models;

namespace SecureShop.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await db.Database.MigrateAsync();

            string[] roles = ["Admin", "Customer"];
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Create admin user
            var adminEmail = "admin@secureshop.local";
            var admin = await userManager.Users.FirstOrDefaultAsync(u => u.Email == adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "Site Admin"
                };
                await userManager.CreateAsync(admin, "Admin#12345");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            // Add sample products
            if (!await db.Products.AnyAsync())
            {
                db.Products.AddRange(
                    new Product { Name = "Keyboard", Price = 1999, Description = "Compact wired keyboard" },
                    new Product { Name = "Mouse", Price = 999, Description = "Optical mouse" },
                    new Product { Name = "Headset", Price = 3499, Description = "Stereo headset" }
                );
                await db.SaveChangesAsync();
            }
        }
    }
}
