using LocalCommunitySite.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalCommunitySite.Domain.Repositories
{
    public interface IPostRepository
    {
        public Task<int> Create(Post post);

        public Task Delete(Post post);

        public Task<Post> Get(int id);

        public Task<IEnumerable<Post>> GetAll();

        public Task SaveChangesAsync();
    }
}
