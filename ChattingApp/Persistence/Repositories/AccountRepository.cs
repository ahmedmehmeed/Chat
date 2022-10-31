using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Resource.Account;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace ChattingApp.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository 
    {
        private readonly UserManager<AppUsers> userManager;
        private readonly IMapper Mapper;
        private readonly ITokenRepository tokenService;
        public AccountRepository(UserManager<AppUsers> userManager , IMapper mapper,ITokenRepository tokenService)
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

           var user = Mapper.Map<AppUsers>(registerDto);
            user.Created = DateTime.Now;
            user.LastActive=DateTime.Now;

          var result =   await userManager.CreateAsync(user,registerDto.Password);
       
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Code} - {error.Description}";
                }
                return new AuthModel { Message = errors ,IsSuccess=false};
            }
            
           await userManager.AddToRoleAsync(user, "Admin");
            var JwtSecurityToken = await tokenService.CreateJwtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                //TokenExpiration = JwtSecurityToken.ValidTo,
                IsAuthencated = true,
                // Roles = new List<string> {"User"},
                Roles = (List<string>) await userManager.GetRolesAsync(user),
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken),
                Username = user.UserName,
                IsSuccess=true
            };
        }

        public async Task<AuthModel> LoginAsync(LoginDto loginDto)
        {
            AuthModel authModel = new AuthModel();
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, loginDto.password))
            {
                authModel.Message = "Email Or Password invalid";
                authModel.IsSuccess = false;
                return authModel;
            }
            



            var JwtToken = await tokenService.CreateJwtToken(user);
            var UserRoles = await userManager.GetRolesAsync(user);

            authModel.IsAuthencated = true;
            authModel.IsSuccess=true;
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.UserId = user.Id;
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
