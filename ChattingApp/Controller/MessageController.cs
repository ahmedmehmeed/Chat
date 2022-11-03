using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Extensions;
using ChattingApp.Persistence;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Resource.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChattingApp.Controller
{
    [Authorize]
    public class MessageController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MessageController(IUnitOfWork unitOfWork,IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        

        
        [HttpPost("SendMessage")]
        public async Task<ActionResult<MessageDto>> SendMessage([FromBody] SendMessageDto sendMessageDto)
        {
            if (string.IsNullOrEmpty(sendMessageDto.ReceiverUsername))
                return BadRequest();
            var SenderUsername = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var SenderUser = await unitOfWork.UserRepository.GetUserByNameAsync(SenderUsername);
            var ReceiverUser = await unitOfWork.UserRepository.GetUserByNameAsync(sendMessageDto.ReceiverUsername);
            if (ReceiverUser == null) return NotFound();
            if (SenderUsername == sendMessageDto.ReceiverUsername.ToLower())
            return BadRequest("You Cannot Send Messages To Yourself !");
            var message = new Message
            {
                Sender = SenderUser,
                Receiver = ReceiverUser,
                SenderUsername = SenderUsername,
                ReceiverUsername = ReceiverUser.UserName,
                Content = sendMessageDto.Content
            };
            unitOfWork.MessageRepository.AddMeesage(message);
            if (await unitOfWork.Commit())
                return Ok(mapper.Map<MessageDto>(message));
            return BadRequest("Sorry Failed To Send Message");

        }



        [HttpGet("GetMessagesThread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest();
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var messages = await unitOfWork.MessageRepository.GetMessageThreadAsync(currentUsername, username);
            //Response.AddPaginationToHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages);
            return Ok(messages);
        }

        [HttpDelete("DeleteMessage")]
        public async Task<ActionResult> DeleteMessage(Guid id)
        {
            if (id==null)
                return BadRequest();
           var message= await unitOfWork.MessageRepository.GetMessageByIdAsync(id);
            unitOfWork.MessageRepository.DeleteMessage(message);

            if (await unitOfWork.Commit())
                return Ok();
            return BadRequest("Sorry Failed To Delete Message");
        }

        [HttpDelete("DeleteMessagesThread")]
        public async Task<ActionResult> DeleteMessagesThread(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var SenderUser = await unitOfWork.UserRepository.GetUserByNameAsync(currentUsername);


            unitOfWork.MessageRepository.DeleteMessagesThread(SenderUser.Id, userId);

            if (await unitOfWork.Commit())
                return Ok();
            return BadRequest("Sorry Failed To Delete Chat");
        }

    }
}
