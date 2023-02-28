using System.Threading.Tasks;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IServiceRepository ServiceRepository { get; }

        IWeddingRepository WeddingRepository { get; }
        IProviderRepository ProviderRepository { get; }

        Task<int> SaveChanges();
    }
}
