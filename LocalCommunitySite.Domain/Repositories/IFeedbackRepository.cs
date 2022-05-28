using LocalCommunitySite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCommunitySite.Domain.Repositories
{
    public interface IFeedbackRepository
    {
        public Task<Feedback> Create(Feedback feedback);

        public Task Delete(Feedback feedback);

        public Task<Feedback> Get(int id);

        public Task<IEnumerable<Feedback>> GetAll();

        public Task SaveChangesAsync();

        public Task<IEnumerable<Feedback>> GetFiltered(DateTime? startDate, DateTime? endDate);
    }
}
