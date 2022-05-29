using LocalCommunitySite.API.Models.CommentDtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalCommunitySite.API.Services.Interfaces
{
    public interface ICommentService
    {
        IEnumerable<CommentGetDto> GetFiltered(int postId);

        public Task<int> Create(CommentDto comment);

        public Task Delete(int id);

        public Task<CommentGetDto> Get(int id);

        public IEnumerable<CommentGetDto> GetAll();

        public Task Update(int id, CommentDto source);
    }
}
