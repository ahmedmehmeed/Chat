using ChattingApp.Domain.Models;
using ChattingApp.Helper.Security.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ChattingApp.Persistence.Interfaces
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateJwtToken(AppUsers user);
        RefreshToken CreateRefreshToken();

    }
}
