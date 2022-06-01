using LocalCommunitySite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalCommunitySite.Domain.Repositories
{
    public interface IPostRepository
    {
        public Task<Post> Create(Post post);

        public Task Delete(Post post);

        public Task<Post> Get(int id);

        public Task<IEnumerable<Post>> GetAll();

        public Task SaveChangesAsync();

        public Task<IEnumerable<Post>> GetPaged(int pageSize, int pageNumber);

        public Task<IEnumerable<Post>> GetFiltered(string title, PostStatus? status, DateTime? startDate, DateTime? endDate);
    }
}
