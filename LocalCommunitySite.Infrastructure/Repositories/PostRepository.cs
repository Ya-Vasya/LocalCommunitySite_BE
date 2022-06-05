using LocalCommunitySite.Domain.Entities;
using LocalCommunitySite.Domain.Helpers;
using LocalCommunitySite.Domain.Query;
using LocalCommunitySite.Domain.Repositories;
using LocalCommunitySite.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Post> Create(Post post)
        {
            var entity = await _appDbContext.Posts.AddAsync(post);

            return entity.Entity;
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

        public async Task<IEnumerable<Post>> GetFiltered(string title, PostStatus? status, DateTime? startDate, DateTime? endDate)
        {
            var query = _appDbContext.Posts.AsQueryable();

            if(!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }

            if (status != null)
            {
                query = query.Where(x => x.Status == status);
            }

            if(startDate != null && endDate != null)
            {
                if(startDate != endDate)
                {
                    query = query.Where(x => x.CreatedAt > startDate && x.CreatedAt < endDate);
                }
                else
                {
                    query = query.Where(x => x.CreatedAt == startDate);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPaged(int pageSize, int pageNumber)
        {
            var posts = _appDbContext.Posts.GetPaged(pageNumber, pageSize);

            return posts.Results.AsEnumerable();
        }

        public async Task<Pagination<Post>> GetQuery(PostQuery query)
        {
            var posts = _appDbContext.Posts
                .Where(i => query.Status == null || i.Status == query.Status)
                .Where(i => query.Section == null || i.Section == query.Section)
                .Where(i => query.StartDate == null || i.CreatedAt.Date >= query.StartDate.Value.Date)
                .Where(i => query.EndDate == null || i.CreatedAt.Date <= query.EndDate.Value.Date)
                .OrderByDescending(i => i.CreatedAt);

            int length = posts.Count();

            var filtered = await posts.Skip(query.Offset).Take(query.Limit).ToListAsync();

            return new Pagination<Post>{ TotalLength = length, List = filtered };
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
