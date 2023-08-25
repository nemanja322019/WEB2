using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Models;

namespace WebApplication.Infrastructure
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.ItemName).HasMaxLength(20);

            builder.HasOne(x => x.Seller)
                   .WithMany(x => x.Items)
                   .HasForeignKey(x => x.SellerId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
