using SoccerJerseyStore.Models;
using SoccerJerseyStore.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 27))
    ));

// Build the application
var app = builder.Build();

// Apply pending migrations on startup and seed the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        // Apply any pending migrations
        dbContext.Database.Migrate();

        // Seed data if the database is empty
        if (!dbContext.Jerseys.Any())
        {
            dbContext.Jerseys.AddRange(new List<Jersey>
            {
                new Jersey { Name = "Home Jersey", Description = "Official home jersey", Color = "Blue", Size = "L", Price = 59.99m },
                new Jersey { Name = "Away Jersey", Description = "Official away jersey", Color = "White", Size = "M", Price = 49.99m },
                new Jersey { Name = "Third Jersey", Description = "Limited edition third jersey", Color = "Black", Size = "S", Price = 69.99m }
            });

            dbContext.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        // Log the exception (you could use a logging service like Serilog here)
        Console.WriteLine($"An error occurred while migrating or seeding the database: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Detailed error pages for development
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Run the application
app.Run();
