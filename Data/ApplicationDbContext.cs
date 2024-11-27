using Microsoft.EntityFrameworkCore;
using SoccerJerseyStore.Models;

namespace SoccerJerseyStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Jersey> Jerseys { get; set; } = null!;
    }
}
