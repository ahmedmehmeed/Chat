
using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Extensions;
using ChattingApp.Resource.Follow;
using ChattingApp.Resource.User;

namespace ChattingApp.Helper.Mapping
{
    public class DomainToResource:Profile
    {
        public DomainToResource()
        {
            CreateMap<AppUsers, UserResponseDto>()
                .ForMember(dest => dest.PhotoDto, opt => opt.MapFrom(src => src.Photos))
                .ForMember(des => des.age, opt => opt.MapFrom(src => src.BirthDate.CalculateAge()))
                .ForMember(des => des.PhotoURL, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain==true).Url)).ReverseMap();         
            CreateMap<Photo, PhotoDto>();

            CreateMap<AppUsers, FollowDto>()
                .ForMember(des => des.age, opt => opt.MapFrom(src => src.BirthDate.CalculateAge()))
                .ForMember(des => des.PhotoURL, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain == true).Url)).ReverseMap();


        }
    }
}
