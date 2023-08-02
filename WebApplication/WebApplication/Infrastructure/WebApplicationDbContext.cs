using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Infrastructure
{
    public class WebApplicationDbContext : DbContext
    {

       

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Item> Items { get; set; }

        public WebApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebApplicationDbContext).Assembly);
        }

    }
}
