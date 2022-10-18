using ChattingApp.Domain.Models;
using ChattingApp.Helper.Pagination;
using ChattingApp.Resource.Message;

namespace ChattingApp.Persistence.IRepositories
{
    public interface IMessageRepository
    {
        void AddMeesage(Message message);
        void DeleteMeesage(Message message);
        Task<Message> GetMessageAsync(Guid id);
        Task<PagedList <MessageDto>> GetUserMessageAsync(MessageReqDto messageReqDto);
        PagedList<MessageDto> GetReceiverMessage(MessageReqDto messageReqDto);
        PagedList<MessageDto> GetSenderMessages(MessageReqDto messageReqDto);
        Task<IEnumerable<MessageDto>> GetMessageThreadAsync(string currentUsername,string recieverUsername);
        Task<bool> SaveAllChangesAsync();

    }
}
