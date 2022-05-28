using LocalCommunitySite.API.Extentions.Exceptions;
using LocalCommunitySite.API.Models.PostDtos;
using LocalCommunitySite.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LocalCommunitySite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _postService.Get(id));
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetAll());
        }


        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Create([FromBody] PostDto post)
        {
            _ = post ?? throw new BadRequestException(nameof(post));

            return Ok(await _postService.Create(post));
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.Delete(id);

            return Ok();
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] PostDto source)
        {
            _ = source ?? throw new BadRequestException(nameof(source));

            await _postService.Update(id, source);

            return Ok();
        }

        [HttpPost("filter")]
        //[Authorize]
        public async Task<IActionResult> GetFiltered([FromBody] PostFilterRequest filterRequest)
        {
            _ = filterRequest ?? throw new BadRequestException(nameof(filterRequest));

            return Ok(await _postService.GetFiltered(filterRequest));
        }
    }
}
