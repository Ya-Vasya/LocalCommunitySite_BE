using AutoMapper;
using LocalCommunitySite.API.Extentions.Exceptions;
using LocalCommunitySite.API.Models.CommentDtos;
using LocalCommunitySite.API.Services.Interfaces;
using LocalCommunitySite.Domain.Entities;
using LocalCommunitySite.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalCommunitySite.API.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<int> Create(CommentDto comment)
        {
            var mappedComment = _mapper.Map<CommentDto, Comment>(comment);
            mappedComment.CreatedAt = DateTime.Now;

            var createdComment = await _commentRepository.Create(mappedComment);

            await _commentRepository.SaveChangesAsync();

            return createdComment.Id;
        }

        public async Task Delete(int id)
        {
            var commentToDelete = await _commentRepository.Get(id);

            _ = commentToDelete ?? throw new NotFoundException($"Object with id: {id} not found");

            await _commentRepository.Delete(commentToDelete);

            await _commentRepository.SaveChangesAsync();
        }

        public async Task<CommentGetDto> Get(int id)
        {
            var comment = await _commentRepository.Get(id);

            _ = comment ?? throw new NotFoundException($"Object with id: {id} not found");

            return _mapper.Map<Comment, CommentGetDto>(comment);
        }

        public IEnumerable<CommentGetDto> GetAll()
        {
            var comment = _commentRepository.GetAll();

            return _mapper.Map<IQueryable<Comment>, IEnumerable<CommentGetDto>>(comment);
        }

        public IEnumerable<CommentGetDto> GetFiltered(int postId)
        {
            var comments = _commentRepository.GetFiltered(postId).OrderByDescending(i => i.CreatedAt);

            return _mapper.Map<IQueryable<Comment>, IEnumerable<CommentGetDto>>(comments);
        }

        public async Task Update(int id, CommentDto source)
        {
            _ = source ?? throw new ObjectNullException($"{nameof(source)} is null");

            var comment = await _commentRepository.Get(id);

            _ = comment ?? throw new NotFoundException($"Object with id: {id} not found");

            _mapper.Map(source, comment);
            comment.UpdatedAt = DateTime.Now;

            await _commentRepository.SaveChangesAsync();
        }
    }
}
