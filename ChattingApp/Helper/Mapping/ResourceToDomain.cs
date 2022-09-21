using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Resource.Account;

namespace ChattingApp.Helper.Mapping

{
    public class ResourceToDomain:Profile
    {

        public ResourceToDomain()
        {
            CreateMap<RegisterDto, AppUsers>()
               .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src =>src.Password));
        }
    }
}
