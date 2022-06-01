using AutoMapper;
using LocalCommunitySite.API.Extentions.Exceptions;
using LocalCommunitySite.API.Models.CommentDtos;
using LocalCommunitySite.API.Models.PostDtos;
using LocalCommunitySite.API.Models.Shared;
using LocalCommunitySite.API.Services.Interfaces;
using LocalCommunitySite.Domain.Entities;
using LocalCommunitySite.Domain.Query;
using LocalCommunitySite.Domain.Repositories;
using LocalCommunitySite.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalCommunitySite.API.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, ICommentRepository commentRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<int> Create(PostDto source)
        {
            _ = source ?? throw new ObjectNullException($"{nameof(source)} is null");

            var post = _mapper.Map<PostDto, Post>(source);

            var createdPost = await _postRepository.Create(post);
            createdPost.CreatedAt = DateTime.Now;

            await _postRepository.SaveChangesAsync();

            return createdPost.Id;
        }

        public async Task Delete(int id)
        {
            Post postToDelete = await _postRepository.Get(id);

            _ = postToDelete ?? throw new NotFoundException($"Object with id: {id} not found");

            await _postRepository.Delete(postToDelete);

            await _postRepository.SaveChangesAsync();
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

            return posts.Select(x => _mapper.Map<PostGetDto>(x)).OrderByDescending(x => x.CreatedAt);
        }

        public async Task Update(int id, PostDto source)
        {
            _ = source ?? throw new ObjectNullException($"{nameof(source)} is null");

            Post post = await _postRepository.Get(id);

            _ = post ?? throw new NotFoundException($"Object with id: {id} not found");

            _mapper.Map(source, post);

            await _postRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostGetDto>> GetFiltered(PostFilterRequest filterRequest)
        {
            _ = filterRequest ?? throw new ObjectNullException($"{nameof(filterRequest)} is null");

            var posts = await _postRepository.GetFiltered(
                filterRequest.Title,
                filterRequest.Status,
                filterRequest.StartDate,
                filterRequest.EndDate);

            return posts.Select(x => _mapper.Map<PostGetDto>(x)).OrderByDescending(x => x.CreatedAt);
        }

        public async Task<PaginationDto<PostGetDto>> GetQuery(PostQueryDto query)
        {
            var mappedQuery = _mapper.Map<PostQueryDto, PostQuery>(query);

            return _mapper.Map<Pagination<Post>, PaginationDto<PostGetDto>>(await _postRepository.GetQuery(mappedQuery));
        }
    }
}
