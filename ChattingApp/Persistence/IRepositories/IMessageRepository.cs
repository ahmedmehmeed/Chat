using ChattingApp.Domain.Models;
using ChattingApp.Helper.Pagination;
using ChattingApp.Resource.Message;

namespace ChattingApp.Persistence.IRepositories
{
    public interface IMessageRepository
    {
        void AddMeesage(Message message);
        void DeleteMessage(Message message);
        void DeleteMessagesThread(string senderId , string receiverId);
        Task<Message> GetMessageByIdAsync(Guid id);
        Task<PagedList <MessageDto>> GetUserMessageAsync(MessageReqDto messageReqDto);
        Task<List<IEnumerable<MessageDto>>> GetMessageThreadAsync(string currentUsername,string recieverUsername);
        Task<List<IEnumerable<MessageDto>>> GetAllChatsAsync(MessageReqDto messageReqDto);

    }
}
