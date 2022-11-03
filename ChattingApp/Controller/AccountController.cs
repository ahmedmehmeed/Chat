using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Helper.Security.Tokens;
using ChattingApp.Persistence;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Persistence.IServices;
using ChattingApp.Resource.Account;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IAccountRepository accountService;
        private readonly IMessagerService messagerService;

        public AccountController(IAccountRepository accountService, IMessagerService messagerService)
        {
            this.accountService = accountService;
            this.messagerService = messagerService;
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
            // return Ok(new { Token = result.Token, Roles = result.Roles, ExpireOn = result.RefreshTokenEXpiration });
             return Ok(result);

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


        [HttpPost("Confirm-Email")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string token, string userid)
        {
            if (await messagerService.ConfirmEmailAsync(token, userid))
                return Ok();
            return BadRequest();

        }
    }
}
