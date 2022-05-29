using LocalCommunitySite.API.Models.PostDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalCommunitySite.API.Services.Interfaces
{
    public interface IPostService
    {
        public Task<int> Create(PostDto post);

        public Task Delete(int id);

        public Task<PostGetDto> Get(int id);

        public Task<IEnumerable<PostGetDto>> GetAll();

        public Task<IEnumerable<PostGetDto>> GetFiltered(PostFilterRequest filterRequest);

        public Task Update(int id, PostDto source);
    }
}
