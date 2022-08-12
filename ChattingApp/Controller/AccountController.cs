using AutoMapper;
using ChattingApp.Controllers___Copy__2_;
using ChattingApp.Domain.Models;
using ChattingApp.Resource.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ChattingApp.Controller
{

    public class AccountController : BaseApiController
    {
        private readonly AppDbContext dbContxt;
        private readonly IMapper mapper;

        public AccountController(AppDbContext dbContxt, IMapper mapper)
        {
            this.dbContxt = dbContxt;
            this.mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AppUsers>> Register([FromBody] RegisterDto registerDto)
        {
            if (await userExist(registerDto.name)) 
                    return BadRequest("user is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUsers
            {
                Username = registerDto.name,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                PasswordSalt = hmac.Key
            };

            dbContxt.Add(user);
            await dbContxt.SaveChangesAsync();
            return user;
        }

        public async Task< bool> userExist(String username)
        {
            return await dbContxt.Users.AnyAsync(x => x.Username == username);
        }
    }
}
