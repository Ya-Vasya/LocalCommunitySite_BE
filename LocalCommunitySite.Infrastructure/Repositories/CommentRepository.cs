using LocalCommunitySite.Domain.Entities;
using LocalCommunitySite.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCommunitySite.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _appDbContext;

        public CommentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Comment> Create(Comment comment)
        {
            var entity = await _appDbContext.AddAsync(comment);
            return entity.Entity;
        }

        public async Task Delete(Comment comment)
        {
            _appDbContext.Remove(comment);
        }

        public async Task<Comment> Get(int id)
        {
            return await _appDbContext.Comments.FindAsync(id);
        }

        public IQueryable<Comment> GetAll()
        {
            return _appDbContext.Comments;
        }

        public IQueryable<Comment> GetFiltered(int postId)
        {
            return _appDbContext.Comments
                .Where(i => i.PostId == postId && i.ParentCommentId == null);
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
