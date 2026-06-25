namespace Quorum.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
