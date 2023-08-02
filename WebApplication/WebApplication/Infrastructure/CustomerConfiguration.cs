using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Models;

namespace WebApplication.Infrastructure
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasMaxLength(20);
            builder.Property(x => x.LastName).HasMaxLength(20);

            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Username).HasMaxLength(20);

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
