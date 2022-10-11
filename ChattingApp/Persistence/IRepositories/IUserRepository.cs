using ChattingApp.Domain.Models;
using ChattingApp.Helper.Pagination;
using ChattingApp.Resource.User;
using System.Linq;

namespace ChattingApp.Persistence.IRepositories
{
    public interface IUserRepository
    {
        Task<PagedList<AppUsers>> GetUsersAsync(UserReqDto userReqDto);
        Task<UserResponseDto> GetUserByIdAsync(string Id);
        Task<AppUsers> GetUserByNameAsync(string UserName);
        Task<int> UpdateUserAsync(UserUpdateDto userUpdateDto);
        int DeleteUserAsync(string Id);
        Task<bool> SaveChangesAsync();


    }
}
