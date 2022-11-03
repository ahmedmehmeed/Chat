using ChattingApp.Domain.Models;
using ChattingApp.Persistence;
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

        private readonly IUnitOfWork unitOfWork;

        public FollowController(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }

        // GET: api/<UsersController>
        [HttpGet("AddFollow")]

        public async Task<ActionResult> AddFollow(string FollowedId)
        {
            var sourceUsername = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var sourceUser = await unitOfWork.UserRepository.GetUserByNameAsync(sourceUsername);
            var followedUser = await unitOfWork.UserRepository.GetUserByIdAsync(FollowedId);
            if (followedUser == null) return NotFound();
            var userFollow = await unitOfWork.FollowsRepository.GetUserFollow(sourceUser.Id, FollowedId);
            if (userFollow != null) { return BadRequest("you already follow this member"); }
            var newfollowee = new UserFollow
            {
                SourceUserId=sourceUser.Id,
                UserFollowedId = FollowedId
            };

            unitOfWork.FollowsRepository.AddNewFollowee(newfollowee);

            if (await unitOfWork.Commit()) return Ok();
            return BadRequest(" failed to follow user");
        }



        [HttpGet("GetFollows")]
        public async Task<ActionResult> GetFollows(string predicate)
        {
            var sourceUsername = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var sourceUser = await unitOfWork.UserRepository.GetUserByNameAsync(sourceUsername);
            var users = await unitOfWork.FollowsRepository.GetUserFollowers(predicate, sourceUser.Id);
            return Ok(users);
        }
    }
}