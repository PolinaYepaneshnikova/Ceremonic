using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IServiceRepository ServiceRepository { get; }

        IWeddingRepository WeddingRepository { get; }
        IProviderRepository ProviderRepository { get; }

        IFileRepository FileRepository { get; }
        void SetFileRepository(ControllerBase controller, IWebHostEnvironment env);

        Task<int> SaveChanges();
    }
}
