using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Enums;
using WebApplication.Models;

namespace WebApplication.Infrastructure
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasMaxLength(20);
            builder.Property(x => x.LastName).HasMaxLength(20);

            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Username).HasMaxLength(20);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.UserType)
                   .HasConversion(
                       x => x.ToString(),
                       x => Enum.Parse<UserTypes>(x)
                   );
        }
    }
}
