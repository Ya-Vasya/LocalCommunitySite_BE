using LocalCommunitySite.Domain.Entities;
using LocalCommunitySite.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalCommunitySite.Infrastructure.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext _appDbContext;

        public FeedbackRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Feedback> Create(Feedback feedback)
        {
            var entity = await _appDbContext.Feedbacks.AddAsync(feedback);

            return entity.Entity;
        }

        public async Task Delete(Feedback feedback)
        {
            _appDbContext.Feedbacks.Remove(feedback);
        }

        public async Task<Feedback> Get(int id)
        {
            var feedback = await _appDbContext.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);

            return feedback;
        }

        public async Task<IEnumerable<Feedback>> GetAll()
        {
            return _appDbContext.Feedbacks.AsNoTracking();
        }

        public async Task<IEnumerable<Feedback>> GetFiltered(DateTime? startDate, DateTime? endDate)
        {
            var query = _appDbContext.Feedbacks.AsQueryable();

            if (startDate != null && endDate != null)
            {
                if (startDate != endDate)
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

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
