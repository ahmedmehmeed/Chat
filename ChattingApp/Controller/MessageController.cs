using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Extensions;
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
        private readonly IMessageRepository messageRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public MessageController(IMessageRepository messageRepository,IUserRepository userRepository,IMapper mapper )
        {
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        
        [HttpPost("SendMessage")]
        public async Task<ActionResult<MessageDto>> SendMessage([FromBody] SendMessageDto sendMessageDto)
        {
            if (string.IsNullOrEmpty(sendMessageDto.ReceiverUsername))
                return BadRequest();
            var SenderUsername = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var SenderUser = await userRepository.GetUserByNameAsync(SenderUsername);
            var ReceiverUser = await userRepository.GetUserByNameAsync(sendMessageDto.ReceiverUsername);
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
            messageRepository.AddMeesage(message);
            if (await messageRepository.SaveAllChangesAsync())
                return Ok(mapper.Map<MessageDto>(message));
            return BadRequest("Sorry Failed To Send Message");

        }

        //[HttpPost("GetMessages")]
        //public async Task<ActionResult<MessageDto>> GetMessage([FromBody] MessageReqDto messageReqDto)
        //{
        //    if (string.IsNullOrEmpty(messageReqDto.ReceiverUsername))
        //        return BadRequest();
        //    var SenderUsername = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToLower();
        //    var ReceiverUser = await userRepository.GetUserByNameAsync(messageReqDto.ReceiverUsername);
        //    if (ReceiverUser == null) return NotFound();
        //    messageReqDto.SenderUsername = SenderUsername;
        //    var messages =  messageRepository.GetReceiverMessage(messageReqDto);
        //    Response.AddPaginationToHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages);
        //    return Ok(messages);         
        //}



        [HttpGet("GetMessagesThread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest();
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var messages = await messageRepository.GetMessageThreadAsync(currentUsername, username);
            //Response.AddPaginationToHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages);
            return Ok(messages);
        }

        [HttpDelete("DeleteMessage")]
        public async Task<ActionResult> DeleteMessage(Guid id)
        {
            if (id==null)
                return BadRequest();
           var message= await messageRepository.GetMessageByIdAsync(id);
                       messageRepository.DeleteMessage(message);

            if (await messageRepository.SaveAllChangesAsync())
                return Ok();
            return BadRequest("Sorry Failed To Delete Message");
        }

        [HttpDelete("DeleteMessagesThread")]
        public async Task<ActionResult> DeleteMessagesThread(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var SenderUser = await userRepository.GetUserByNameAsync(currentUsername);


            messageRepository.DeleteMessagesThread(SenderUser.Id, userId);

            if (await messageRepository.SaveAllChangesAsync())
                return Ok();
            return BadRequest("Sorry Failed To Delete Chat");
        }

    }
}
