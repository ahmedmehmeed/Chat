using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Helper.Security.Tokens;
using ChattingApp.Persistence;
using ChattingApp.Persistence.Interface;
using ChattingApp.Resource.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ChattingApp.Controller
{

    public class AccountController : BaseApiController
    {
        private readonly IAccountService accountService;


        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;

        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
         var result =   await accountService.RegisterAsync(registerDto);
            if (!result.IsAuthencated)
                return BadRequest(result.Message);
            return Ok(new{ token = result.Token});

        }

        //private async Task< bool> userExist(String username)
        //{
        //    return await dbContxt.Users.AnyAsync(x => x.Username == username);
        //}



        [HttpPost("Login")]
        public async Task<ActionResult<AppUsers>> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(loginDto);
            AuthModel result = await accountService.LoginAsync(loginDto);

            //IsAuthencated ==>return false as default if don't occure any creation for user 
            if (!result.IsAuthencated)
                return BadRequest(result.Message);
            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenEXpiration);
            // anomnyous object
            return Ok(new { Token = result.Token, Roles = result.Roles, ExpireOn = result.RefreshTokenEXpiration });
        }

        private void SetRefreshTokenInCookies(string refreshTokenModel, DateTime ExpireOn)
        {
            //setting cookies 
            CookieOptions cookiesOption = new()
            {
                HttpOnly = true,
                Expires = ExpireOn.ToLocalTime(),

            };

            Response.Cookies.Append("RefreshToken", refreshTokenModel, cookiesOption);

        }

    }
}
