using AutoMapper;
using LocalCommunitySite.API.Extentions.Exceptions;
using LocalCommunitySite.API.Models.FeedbackDtos;
using LocalCommunitySite.API.Services.Interfaces;
using LocalCommunitySite.Domain.Entities;
using LocalCommunitySite.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalCommunitySite.API.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;

        public FeedbackService(IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
        }

        public async Task<int> Create(FeedbackRequest source)
        {
            _ = source ?? throw new ObjectNullException($"{nameof(source)} is null");

            Feedback feedback = new Feedback();
            feedback.Name = source.Name;
            feedback.Email = source.Email;
            feedback.PhoneNumber = source.PhoneNumber;
            feedback.Text = source.Text;
            feedback.CreatedAt = DateTime.Now;

            var createdPost = await _feedbackRepository.Create(feedback);

            await _feedbackRepository.SaveChangesAsync();

            return createdPost.Id;
        }

        public async Task Delete(int id)
        {
            Feedback feedback = await _feedbackRepository.Get(id);

            _ = feedback ?? throw new NotFoundException($"Object with id: {id} not found");

            await _feedbackRepository.Delete(feedback);

            await _feedbackRepository.SaveChangesAsync();
        }

        public async Task<FeedbackDto> Get(int id)
        {
            Feedback feedback = await _feedbackRepository.Get(id);

            _ = feedback ?? throw new NotFoundException($"Object with id: {id} not found");

            return _mapper.Map<FeedbackDto>(feedback);
        }

        public async Task<IEnumerable<FeedbackDto>> GetAll()
        {
            var feedbacks = await _feedbackRepository.GetAll();

            return feedbacks.Select(x => _mapper.Map<FeedbackDto>(x)).OrderByDescending(x => x.Id);
        }

        public async Task<IEnumerable<FeedbackDto>> GetFiltered(FeedbackFilterRequest filterRequest)
        {
            _ = filterRequest ?? throw new ObjectNullException($"{nameof(filterRequest)} is null");

            var posts = await _feedbackRepository.GetFiltered(
                filterRequest.StartDate,
                filterRequest.EndDate);

            return posts.Select(x => _mapper.Map<FeedbackDto>(x)).OrderByDescending(x => x.Id);
        }
    }
}
