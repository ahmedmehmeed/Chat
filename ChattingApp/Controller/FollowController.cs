using ChattingApp.Domain.Models;
using ChattingApp.Persistence.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChattingApp.Controller
{
    [Authorize]
    public class FollowController : BaseApiController
    {
        private readonly IFollowsRepository followRepository;
        private readonly IUserRepository userRepository;

        public FollowController(IFollowsRepository followRepository, IUserRepository userRepository)
        {
            this.followRepository = followRepository;
            this.userRepository = userRepository;
        }

        // GET: api/<UsersController>
        [HttpPost("AddFollow")]

        public async Task<ActionResult> AddFollow(string FollowedId)
        {
            var sourceUsername = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var sourceUser = await userRepository.GetUserByNameAsync(sourceUsername);
            var followedUser = await userRepository.GetUserByIdAsync(FollowedId);
            if (followedUser == null) return NotFound();
            var userFollow = await followRepository.GetUserFollow(sourceUser.Id, FollowedId);
            if (userFollow != null) return BadRequest("you already follow this member");
            userFollow = new UserFollow
            {
                SourceUserId = sourceUser.Id,
                UserFollowedId = FollowedId
            };
            sourceUser.Followees.Add(userFollow);
            if (await userRepository.SaveChangesAsync()) return Ok();
            return BadRequest(" failed to follow user");
        }


        [HttpGet("GetFollows")]
        public async Task<ActionResult> GetFollows(string predicate)
        {
            var sourceUsername = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var sourceUser = await userRepository.GetUserByNameAsync(sourceUsername);
            var users = await  followRepository.GetUserFollowers(predicate, sourceUser.Id);
            return Ok(users);
        }
    }
}