using ChattingApp.Persistence.IRepositories;

namespace ChattingApp.Persistence
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IFollowsRepository FollowsRepository { get; }
        IMessageRepository MessageRepository { get; }

        Task<bool> Commit();

        bool HasChanges();

    }
}
