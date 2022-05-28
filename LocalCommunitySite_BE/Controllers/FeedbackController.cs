using LocalCommunitySite.API.Extentions.Exceptions;
using LocalCommunitySite.API.Models.FeedbackDtos;
using LocalCommunitySite.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LocalCommunitySite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _feedbackService.Get(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _feedbackService.GetAll());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FeedbackRequest feedback)
        {
            _ = feedback
                ?? throw new BadRequestException(nameof(feedback));

            return Ok(await _feedbackService.Create(feedback));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _feedbackService.Delete(id);

            return Ok();
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetFiltered([FromBody] FeedbackFilterRequest filterRequest)
        {
            _ = filterRequest ?? throw new BadRequestException(nameof(filterRequest));

            return Ok(await _feedbackService.GetFiltered(filterRequest));
        }
    }
}
