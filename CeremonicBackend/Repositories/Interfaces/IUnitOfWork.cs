using System.Threading.Tasks;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        Task<int> SaveChanges();
    }
}
