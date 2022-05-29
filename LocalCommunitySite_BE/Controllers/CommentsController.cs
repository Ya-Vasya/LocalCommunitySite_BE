using LocalCommunitySite.API.Extentions.Exceptions;
using LocalCommunitySite.API.Models.CommentDtos;
using LocalCommunitySite.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LocalCommunitySite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _commentService.Get(id));
        }

        [HttpGet]
        //[Authorize]
        public IActionResult GetAll()
        {
            return Ok(_commentService.GetAll());
        }

        [HttpGet("post/{postId}")]
        public IActionResult Get(int postId)
        {
            var comments = _commentService.GetFiltered(postId);

            return Ok(comments);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Create([FromBody] CommentDto comment)
        {
            _ = comment ?? throw new BadRequestException(nameof(comment));

            return Ok(await _commentService.Create(comment));
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _commentService.Delete(id);

            return Ok();
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] CommentDto source)
        {
            _ = source ?? throw new BadRequestException(nameof(source));

            await _commentService.Update(id, source);

            return Ok();
        }
    }
}
