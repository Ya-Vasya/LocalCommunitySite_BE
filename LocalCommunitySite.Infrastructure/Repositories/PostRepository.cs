using LocalCommunitySite.Domain.Entities;
using LocalCommunitySite.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalCommunitySite.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _appDbContext;

        public PostRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Create(Post post)
        {
            var entity = await _appDbContext.Posts.AddAsync(post);

            return entity.Entity.Id;
        }

        public async Task Delete(Post post)
        {
            _appDbContext.Posts.Remove(post);
        }

        public async Task<Post> Get(int id)
        {
            var post = await _appDbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);

            return post;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return _appDbContext.Posts.AsNoTracking();
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
