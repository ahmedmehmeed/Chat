using ChattingApp.Domain.Models;
using ChattingApp.Helper.Security.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ChattingApp.Persistence.IRepositories
{
    public interface ITokenRepository
    {
        Task<JwtSecurityToken> CreateJwtToken(AppUsers user);
        RefreshToken CreateRefreshToken();

    }
}
