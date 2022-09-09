using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Persistence.Interface;
using ChattingApp.Persistence.Interfaces;
using ChattingApp.Resource.Account;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace ChattingApp.Persistence.Services
{
    public class AccountService : IAccountService 
    {
        private readonly UserManager<AppUsers> userManager;
        private readonly IMapper Mapper;
        private readonly ITokenService tokenService;
        public AccountService(UserManager<AppUsers> userManager , IMapper mapper,ITokenService tokenService)
        {
            this.userManager = userManager;
            Mapper = mapper;
           this.tokenService = tokenService;
        }

        public async Task<AuthModel> RegisterAsync(RegisterDto registerDto)
        {
            AuthModel authModel = new AuthModel();

             if(  await  userManager.FindByEmailAsync(registerDto.Email) is not null)
                  return new AuthModel { Message = "Email is already registerd! " };
            if (await userManager.FindByNameAsync(registerDto.UserName) is not null) 
                   return new AuthModel { Message = "Username is already registerd! " };

           var user =   Mapper.Map<AppUsers>(registerDto);

          var result =   await userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Code} - {error.Description}";
                }
                return new AuthModel { Message = errors };
            }
            
           await userManager.AddToRoleAsync(user, "User");
            var JwtSecurityToken = await tokenService.CreateJwtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                //TokenExpiration = JwtSecurityToken.ValidTo,
                IsAuthencated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken),
                Username = user.UserName
            };


        }

        public async Task<AuthModel> LoginAsync(LoginDto loginDto)
        {
            AuthModel authModel = new AuthModel();
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, loginDto.password))
            {
                authModel.Message = "Email Or Password invalid";
                return authModel;
            }
            



            var JwtToken = await tokenService.CreateJwtToken(user);
            var UserRoles = await userManager.GetRolesAsync(user);

            authModel.IsAuthencated = true;
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(JwtToken);
            // authModel.TokenExpiration = JwtToken.ValidTo;
            authModel.Roles = UserRoles.ToList();
            //check if user have active refresh token
            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var ActiveRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authModel.RefreshToken = ActiveRefreshToken?.Token;
                authModel.RefreshTokenEXpiration = ActiveRefreshToken.ExpiresOn;
            }
            else
            {
                var NewRefreshToken =  tokenService.CreateRefreshToken();
                authModel.RefreshToken = NewRefreshToken?.Token;
                authModel.RefreshTokenEXpiration = NewRefreshToken.ExpiresOn;
                //in memeory
                user.RefreshTokens.Add(NewRefreshToken);
                //save in database 
                await userManager.UpdateAsync(user);
            }

            return authModel;
        }
        




    }
}
