using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Models;

namespace WebApplication.Infrastructure
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => new { x.OrderId, x.ItemId });

            builder.HasOne(x => x.Order)
              .WithMany(x => x.OrderItems)
              .HasForeignKey(x => x.OrderId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Item)
              .WithMany(x => x.OrderItems)
              .HasForeignKey(x => x.ItemId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
