using ChattingApp.Domain.Models;
using ChattingApp.Persistence.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChattingApp.Controller
{
    [Authorize]
    public class UsersController : BaseApiController 
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        // GET: api/<UsersController>
        [HttpGet("GetAllUsers")]
 
        public async Task<ActionResult<IQueryable<AppUsers>>>  Get()
        {
               var Users = await  userRepository.GetUsersAsync();
                return Ok(Users);          
        }


        // GET api/<UsersController>/5
        [HttpGet("GetUserById")]
        public async Task<ActionResult<AppUsers>> Get(string id)
        {
           
                var User = await userRepository.GetUserByIdAsync(id);
            if (User is not null)
                return Ok(User);
            else
                return NotFound();
        }

  

        // PUT api/<UsersController>/5
        [HttpPut("UpdateUSer")]
        public void Put(string id, [FromBody] AppUsers appUsers)
        {
            userRepository.UpdateUserAsync(id, appUsers);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("DeleteUser")]
        public void Delete(string id)
        {
            userRepository.DeleteUserAsync(id);
        }
    }
}
