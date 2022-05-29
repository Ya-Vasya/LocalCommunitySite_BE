using LocalCommunitySite.Domain.Entities;
using LocalCommunitySite.Infrastructure;
using LocalCommunitySite.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LocalCommunitySite.UnitTests
{
    public class CommentRepostoryTests
    {
        [Fact]
        public void GetFiltered()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            using var dbContex = new AppDbContext(options);

            var commentRepository = new CommentRepository(dbContex);

            SeedData(dbContex);

            var a = commentRepository.GetFiltered(1);

        }

        private void SeedData(AppDbContext dbContext)
        {
            var user = new User
            {
                Email = "email1@gmail.com",
                UserName = "username1",
            };

            var post = new Post()
            {
                Title = "post Titile",
                Body = "post Body",
                Status = PostStatus.Active,
                CreatedAt = DateTime.UtcNow,
            };

            var comments = new List<Comment>()
            {
                new Comment
                {
                    Text = "Comment 1",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    PostId = 1,
                    UserId = "1"
                },
                new Comment
                {
                    Text = "Comment 2",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    PostId = 1,
                    UserId = "1",
                    ParentCommentId = 1,
                },
                new Comment
                {
                    Text = "Comment 3",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    PostId = 1,
                    UserId = "1",
                    ParentCommentId = 2,
                },


                new Comment
                {
                    Text = "Comment 4",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    PostId = 1,
                    UserId = "1"
                },
                new Comment
                {
                    Text = "Comment 5",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    PostId = 1,
                    UserId = "1",
                },
                new Comment
                {
                    Text = "Comment 6",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    PostId = 1,
                    UserId = "1",
                }
            };

            dbContext.Users.Add(user);

            dbContext.Posts.Add(post);
            dbContext.SaveChanges();

            foreach (var a in comments)
            {
                dbContext.Comments.Add(a);
                dbContext.SaveChanges();
            } 
        }
    }
}
