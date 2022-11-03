using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Persistence;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Resource.Message;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChattingApp.SignalR
{
    public class MessageHub:Hub
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MessageHub(IUnitOfWork unitOfWork,IMapper mapper)
        {
   
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public override async Task OnConnectedAsync()
        {
            var userName = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"].ToString();
            var groupName = $"{userName}-{otherUser}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var messages = await unitOfWork.MessageRepository.GetMessageThreadAsync(userName, otherUser);

            if (unitOfWork.HasChanges()) await unitOfWork.Commit();
            await Clients.Group(groupName).SendAsync("GetMessagesThread", messages);
        }

        public async Task SendMessage(SendMessageDto sendMessageDto)
        {
            if (string.IsNullOrEmpty(sendMessageDto.ReceiverUsername))
                throw new HubException("Receipted UserName Invalid");
            var SenderUsername = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var SenderUser = await unitOfWork.UserRepository.GetUserByNameAsync(SenderUsername);
            var ReceiverUser = await unitOfWork.UserRepository.GetUserByNameAsync(sendMessageDto.ReceiverUsername);
            if (ReceiverUser == null) throw new HubException("Receipted UserName Not Existed");
            if (SenderUsername == sendMessageDto.ReceiverUsername.ToLower())
                throw new HubException("You Cannot Send Messages To Yourself !");
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
            {
                var groupName = $"{SenderUsername}-{ReceiverUser.UserName}";
                await Clients.Group(groupName).SendAsync("NewMessage", mapper.Map<MessageDto>(message));
            }
              
            throw new HubException("Sorry Failed To Send Message");

        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

    }
}
