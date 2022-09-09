using ChattingApp.Domain.Models;
using ChattingApp.Resource.Account;

namespace ChattingApp.Persistence.Interface
{
    public interface IAccountService
    {
        Task<AuthModel> RegisterAsync(RegisterDto registerDto);

       
        Task<AuthModel> LoginAsync(LoginDto loginDto);
    }
}
