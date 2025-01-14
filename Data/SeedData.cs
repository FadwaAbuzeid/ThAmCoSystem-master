using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThreeAmigosWebApp.Models;
using System.Linq;
using System.Threading.Tasks;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();

        await CreateRolesAsync(roleManager); // Use async/await for role creation
        await SeedProductsAsync(context); // Use async/await for product seeding
        await CreateDefaultAdminUserAsync(userManager, roleManager); // Use async/await for admin user creation
        await CreateDefaultStaffUserAsync(userManager, roleManager); // Use async/await for staff user creation
    }

    private static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var roleNames = new[] { "Admin", "Staff", "Customer" };

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var role = new IdentityRole(roleName);
                await roleManager.CreateAsync(role);
            }
        }
    }

    private static async Task SeedProductsAsync(ApplicationDbContext context)
    {
        if (!context.Products.Any())
        {
            var products = new[]
            {
                new Product
                {
                    Name = "Laptop",
                    Price = 1000,
                    StockQuantity = 50,
                    Description = "A high-performance laptop for gaming and work.",
                    ImageFileName = "laptop.jpg"
                },
                new Product
                {
                    Name = "Smartphone",
                    Price = 500,
                    StockQuantity = 100,
                    Description = "A feature-packed smartphone with excellent camera quality.",
                    ImageFileName = "phone.jpg",
                },
                new Product
                {
                    Name = "Headphones",
                    Price = 150,
                    StockQuantity = 200,
                    Description = "Immerse yourself in crystal-clear sound with these advanced noise-canceling headphones, perfect for music lovers and frequent travelers.",
                    ImageFileName = "headphones.jpg"
                },
                new Product
                {
                    Name = "Shoes",
                    Price = 150,
                    StockQuantity = 200,
                    Description = "Comfortable and stylish shoes for everyday wear.",
                    ImageFileName = "shoes.jpg"
                },
                new Product
                {
                    Name = "Bag",
                    Price = 150,
                    StockQuantity = 200,
                    Description = "A durable and spacious bag for all your needs.",
                    ImageFileName = "bag.jpg"
                }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }

    private static async Task CreateDefaultAdminUserAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        var adminUser = await userManager.FindByEmailAsync("admin@admin.com");
        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                Address = "123 Main St, City, Country",
                PhoneNumber = "1234567890",
                Funds = 100000 // Set initial funds for the admin user
            };

            var result = await userManager.CreateAsync(adminUser, "Admin@123");
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    var role = new IdentityRole("Admin");
                    await roleManager.CreateAsync(role);
                }

                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }

    private static async Task CreateDefaultStaffUserAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        var staffUser = await userManager.FindByEmailAsync("staff@example.com");
        if (staffUser == null)
        {
            staffUser = new User
            {
                UserName = "stafff@example.com",
                Email = "stafff@example.com",
                Address = "456 Staff St, City, Country",
                PhoneNumber = "0987654321",
                Funds = 50000 // Set initial funds for the staff user
            };

            var result = await userManager.CreateAsync(staffUser, "Staff@123");
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("Staff"))
                {
                    var role = new IdentityRole("Staff");
                    await roleManager.CreateAsync(role);
                }

                await userManager.AddToRoleAsync(staffUser, "Staff");
            }
        }
    }
}