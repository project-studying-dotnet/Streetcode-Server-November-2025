using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.Streetcode;

namespace Streetcode.DAL.Configurations
{
    public class CommentsConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasOne(c => c.Streetcode)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.StreetcodeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
