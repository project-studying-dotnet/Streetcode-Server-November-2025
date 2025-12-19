using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.Jwt;

namespace Streetcode.DAL.Configurations
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder
                .HasIndex(t => t.Token).IsUnique();

            builder
                .HasOne(t => t.User)
                .WithMany(t => t.RefreshTokens)
                .HasForeignKey(t => t.UserId);
        }
    }
}
