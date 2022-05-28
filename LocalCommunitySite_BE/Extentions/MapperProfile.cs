using AutoMapper;
using LocalCommunitySite.API.Models.FeedbackDtos;
using LocalCommunitySite.API.Models.PostDtos;
using LocalCommunitySite.API.Models.UserDtos;
using LocalCommunitySite.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LocalCommunitySite.API.Extentions
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostGetDto>().ReverseMap();
            CreateMap<Feedback, FeedbackDto>().ReverseMap();
            CreateMap<Feedback, FeedbackFilterRequest>().ReverseMap();
            CreateMap<Feedback, FeedbackRequest>().ReverseMap();
            CreateMap<IdentityUser, UserDto>().ReverseMap();
            CreateMap<IdentityUser, UserEditDto>().ReverseMap();
        }
    }
}
