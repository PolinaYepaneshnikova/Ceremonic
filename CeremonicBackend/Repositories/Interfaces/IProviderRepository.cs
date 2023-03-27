using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IProviderRepository : IBaseRepository<ProviderEntity, int>
    {
        Task<ProviderEntity> GetByEmail(string email);
    }
}
