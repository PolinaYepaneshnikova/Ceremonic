using System.Threading.Tasks;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IWeddingRepository WeddingRepository { get; }

        Task<int> SaveChanges();
    }
}
