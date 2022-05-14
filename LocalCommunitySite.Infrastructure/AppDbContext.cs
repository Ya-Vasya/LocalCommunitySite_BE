using LocalCommunitySite.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LocalCommunitySite.Infrastructure
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        //DbSets:
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<Post> Posts { get; set; }
    }
}