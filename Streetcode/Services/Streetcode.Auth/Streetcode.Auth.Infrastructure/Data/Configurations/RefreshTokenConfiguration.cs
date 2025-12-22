using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.Auth.Domain.Entities.Auth;

namespace Streetcode.Auth.Infrastructure.Data.Configurations
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder
                .HasKey(r => r.Id);

            builder
                .Property(r => r.Token)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasIndex(r => r.Token)
                .IsUnique();

            builder
                .Property(r => r.ExpiresOn)
                .IsRequired();

            builder
                .Property(r => r.IsRevoked)
                .HasDefaultValue(false);

            builder
                .Property(r => r.UserId)
                .HasMaxLength(450)
                .IsRequired();

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
