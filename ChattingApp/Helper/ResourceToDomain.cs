using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Resource.Account;

namespace ChattingApp.Helper
{
    public class ResourceToDomain:Profile
    {

        public ResourceToDomain()
        {
            //CreateMap<RegisterDto, AppUsers>()
            //    .ForMember(p => p.PasswordHash, opt => opt.MapFrom(x => x.password));
        }
    }
}
