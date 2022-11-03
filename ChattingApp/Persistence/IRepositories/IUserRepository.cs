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
        Task UpdateUserAsync(UserUpdateDto userUpdateDto);
        void DeleteUserAsync(string Id);

    }
}
