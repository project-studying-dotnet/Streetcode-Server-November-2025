using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Streetcode.Auth.Domain.Entities.Auth;
using Streetcode.Auth.Domain.Entities.Users;
using System.Reflection;

namespace Streetcode.Auth.Infrastructure.Data
{
    public class UsersDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public UsersDbContext()
        {
        }

        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.UseCollation("SQL_Ukrainian_CP1251_CI_AS");

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
