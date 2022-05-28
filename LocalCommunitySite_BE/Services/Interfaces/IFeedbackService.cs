using LocalCommunitySite.API.Models.FeedbackDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalCommunitySite.API.Services.Interfaces
{
    public interface IFeedbackService
    {
        public Task<int> Create(FeedbackRequest post);

        public Task Delete(int id);

        public Task<FeedbackDto> Get(int id);

        public Task<IEnumerable<FeedbackDto>> GetAll();

        public Task<IEnumerable<FeedbackDto>> GetFiltered(FeedbackFilterRequest filterRequest);
    }
}
