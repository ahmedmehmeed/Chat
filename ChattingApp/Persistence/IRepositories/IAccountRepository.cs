using ChattingApp.Domain.Models;
using ChattingApp.Resource.Account;

namespace ChattingApp.Persistence.IRepositories
{
    public interface IAccountRepository
    {
        Task<AuthModel> RegisterAsync(RegisterDto registerDto);

       
        Task<AuthModel> LoginAsync(LoginDto loginDto);
    }
}
