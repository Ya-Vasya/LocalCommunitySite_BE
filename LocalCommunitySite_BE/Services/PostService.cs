using AutoMapper;
using LocalCommunitySite.API.Extentions.Exceptions;
using LocalCommunitySite.API.Models.PostDtos;
using LocalCommunitySite.Domain.Entities;
using LocalCommunitySite.Domain.Interfaces.Services;
using LocalCommunitySite.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalCommunitySite.API.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<int> Create(PostDto source)
        {
            _ = source ?? throw new ObjectNullException($"{nameof(source)} is null");

            Post post = new Post();
            post.Title = source.Title;
            post.Body = source.Body;
            post.CreatedAt = DateTime.Now;

            return await _postRepository.Create(post);
        }

        public async Task Delete(int id)
        {
            Post postToDelete = await _postRepository.Get(id);

            _ = postToDelete ?? throw new NotFoundException($"Object with id: {id} not found");

            await _postRepository.Delete(postToDelete);
        }

        public async Task<PostGetDto> Get(int id)
        {
            Post post = await _postRepository.Get(id);

            _ = post ?? throw new NotFoundException($"Object with id: {id} not found");

            return _mapper.Map<PostGetDto>(post);
        }

        public async Task<IEnumerable<PostGetDto>> GetAll()
        {
            var posts = await _postRepository.GetAll();

            return posts.Select(x => _mapper.Map<PostGetDto>(x));
        }

        public async Task Update(int id, PostDto source)
        {
            _ = source ?? throw new ObjectNullException($"{nameof(source)} is null");

            Post post = await _postRepository.Get(id);

            _ = post ?? throw new NotFoundException($"Object with id: {id} not found");

            post.Title = source.Title;
            post.Body = source.Body;
            post.CreatedAt = source.CreatedAt;

            await _postRepository.SaveChangesAsync();
        }
    }
}
