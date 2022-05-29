using LocalCommunitySite.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LocalCommunitySite.Infrastructure
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Comment
            builder.Entity<Comment>()
                .HasOne(i => i.User)
                .WithMany(i => i.Comments)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .Property(i => i.UserId)
                .IsRequired(false);

            base.OnModelCreating(builder);
        }

        //DbSets:
        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Feedback> Feedbacks { get; set; }
    }
}