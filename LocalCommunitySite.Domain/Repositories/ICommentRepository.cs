using LocalCommunitySite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCommunitySite.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task<Comment> Create(Comment comment);

        Task Delete(Comment comment);

        Task<Comment> Get(int id);

        IQueryable<Comment> GetAll();

        IQueryable<Comment> GetFiltered(int postId);

        Task SaveChangesAsync();
    }
}
