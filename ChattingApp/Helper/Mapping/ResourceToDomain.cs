using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Resource.Account;
using ChattingApp.Resource.User;

namespace ChattingApp.Helper.Mapping

{
    public class ResourceToDomain:Profile
    {
    

        public ResourceToDomain()
        {
            CreateMap<RegisterDto, AppUsers>()
               .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src =>src.Password));
            CreateMap<UserUpdateDto, AppUsers>()
                   .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.PhotoDto));
            CreateMap<UserResponseDto, AppUsers>()
                     .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.PhotoDto));



        }
    }
}
