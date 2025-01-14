using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThreeAmigosWebApp.Models;
using ThreeAmigosWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext for the application
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with custom options
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Disable email confirmation for simplicity
    options.Lockout.AllowedForNewUsers = false; // Disable lockout for new users
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Add distributed memory cache for session management
builder.Services.AddDistributedMemoryCache();

// Configure session options
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
});

// Register custom services
builder.Services.AddScoped<IOrderService, OrderService>();

// Add support for controllers and views
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Seed the database with initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        // Get required services
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Seed the database
        await SeedData.Initialize(services, userManager, roleManager);
    }
    catch (Exception ex)
    {
        // Log any errors that occur during seeding
        Console.WriteLine($"An error occurred seeding the database: {ex.Message}");
    }
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Use custom error page in production
    app.UseHsts(); // Enable HTTP Strict Transport Security (HSTS)
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles(); // Serve static files (e.g., CSS, JS, images)

app.UseRouting(); // Enable routing

app.UseAuthentication(); // Enable authentication
app.UseAuthorization(); // Enable authorization

// Configure default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Enable Razor Pages

app.UseSession(); // Enable session management

app.Run(); // Start the application