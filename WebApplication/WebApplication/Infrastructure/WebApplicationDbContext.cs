using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Infrastructure
{
    public class WebApplicationDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }

        public WebApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebApplicationDbContext).Assembly);
        }

    }
}
