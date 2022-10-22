using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChattingApp.Domain.Models;
using ChattingApp.Helper.Pagination;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Resource.Message;
using Microsoft.EntityFrameworkCore;

namespace ChattingApp.Persistence.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public MessageRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        public void AddMeesage(Message message)
        {
            appDbContext.Messages.Add(message);
        }

        public void DeleteMessage(Message message) 
        {
            appDbContext.Messages.Remove(message);
        }

        public void DeleteMessagesThread(string senderId, string receiverId)
        {
            var messages =  appDbContext.Messages
                              .Where(
                                     m => m.SenderId == senderId && m.ReceiverId == receiverId
                                    || m.SenderId == receiverId && m.ReceiverId == senderId ).ToList();
            foreach (var message in messages)
            {
                this.DeleteMessage(message);
            }
        }

        public  async Task<Message> GetMessageByIdAsync(Guid id) 
        {
            return await appDbContext.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }


        //public PagedList<MessageDto> GetReceiverMessage(MessageReqDto messageReqDto)
        //{
        //    var RecieverMessages = appDbContext.Messages
        //                             .Where(m => m.ReceiverUsername == messageReqDto.ReceiverUsername)
        //                             .Where(m => m.SenderUsername == messageReqDto.SenderUsername)
        //                             .Include(a => a.Sender.Photos)
        //                             .Include(a => a.Receiver.Photos)
        //                             .OrderByDescending(m => m.DateMessageSent);

        //    //var RecieverMessagesMapped = mapper.Map<IEnumerable<MessageDto>>(RecieverMessages).AsQueryable();
        //    var RecieverMessagesMapped = RecieverMessages.ProjectTo<MessageDto>(mapper.ConfigurationProvider); //Querable extension method make mapping in database not in memory to select only Dto 
        //    return PagedList<MessageDto>.ToPagedList(RecieverMessagesMapped, messageReqDto.PageSize, messageReqDto.PageNumber);
        //}


        public Task<PagedList<MessageDto>> GetUserMessageAsync(MessageReqDto messageReqDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IEnumerable<MessageDto>>> GetMessageThreadAsync(string currentUsername, string recieverUsername)
        {
            var messages = await appDbContext.Messages
                               .Include(s => s.Sender).ThenInclude(p => p.Photos)
                               .Include(r => r.Receiver).ThenInclude(p => p.Photos)
                               .Where(
                                      m => m.SenderUsername == currentUsername && m.ReceiverUsername == recieverUsername
                                     || m.SenderUsername == recieverUsername && m.ReceiverUsername == currentUsername).OrderBy(k => k.DateMessageSent).ToListAsync();
                    var unreadMessages = messages.Where(m => m.DateRead == null && m.ReceiverUsername == currentUsername).ToList();
                    if (unreadMessages.Any())
                    {
                        foreach (var message in unreadMessages)
                        {
                            message.DateRead = DateTime.Now;

                        }
                        await appDbContext.SaveChangesAsync();
                    }


             var messageDto= new List<IEnumerable<MessageDto>>();
            var mapped = mapper.Map<IEnumerable<MessageDto>>(messages).ToList();
            var messagesMapped = mapped.ToLookup(k => k.DateMessageSent.ToString("MM/dd/yyyy"));
            foreach (var g in messagesMapped)
            {
                 
              messageDto.Add(g.OrderBy(k=>k.DateMessageSent).Select(m => m));

            }
            return messageDto;
        }

        public async Task<List<IEnumerable<MessageDto>>> GetAllChatsAsync(MessageReqDto messageReqDto)
        {
            var messages = await appDbContext.Messages
                   .Include(s => s.Sender).ThenInclude(p => p.Photos)
                   .Include(r => r.Receiver).ThenInclude(p => p.Photos)
                   .Where(
                          m => m.SenderUsername == messageReqDto.CurrentUsername
                         || m.ReceiverUsername == messageReqDto.CurrentUsername).OrderBy(k => k.DateMessageSent).ToListAsync();

            var messageDto = new List<IEnumerable<MessageDto>>();
            var mapped = mapper.Map<IEnumerable<MessageDto>>(messages).ToList();
            var messagesMapped = mapped.ToLookup(k => k.DateMessageSent.ToString("MM/dd/yyyy"));
            foreach (var g in messagesMapped)
            {

                messageDto.Add(g.OrderBy(k => k.DateMessageSent).Select(m => m));

            }
            return messageDto;

        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await appDbContext.SaveChangesAsync() > 0;
        }


    }
}
