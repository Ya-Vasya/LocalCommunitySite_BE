using AutoMapper;
using LocalCommunitySite.API.Models.PostDtos;
using LocalCommunitySite.Domain.Entities;

namespace LocalCommunitySite.API.Extentions
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostGetDto>().ReverseMap();
        }
    }
}
