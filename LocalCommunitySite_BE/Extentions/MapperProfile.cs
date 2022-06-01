using AutoMapper;
using LocalCommunitySite.API.Models.CommentDtos;
using LocalCommunitySite.API.Models.FeedbackDtos;
using LocalCommunitySite.API.Models.PostDtos;
using LocalCommunitySite.API.Models.Shared;
using LocalCommunitySite.API.Models.UserDtos;
using LocalCommunitySite.Domain.Entities;
using LocalCommunitySite.Domain.Query;
using LocalCommunitySite.Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace LocalCommunitySite.API.Extentions
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostGetDto>().ReverseMap();
            CreateMap<PostQuery, PostQueryDto>().ReverseMap();
            CreateMap<PaginationDto<PostGetDto>, Pagination<Post>>().ReverseMap();
            CreateMap<Feedback, FeedbackDto>().ReverseMap();
            CreateMap<Feedback, FeedbackFilterRequest>().ReverseMap();
            CreateMap<Feedback, FeedbackRequest>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserEditDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Comment, CommentGetDto>().ReverseMap();
        }
    }
}
