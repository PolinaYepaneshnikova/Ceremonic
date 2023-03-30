using CeremonicBackend.DB.Mongo;
using System.Threading.Tasks;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IWeddingRepository : IBaseRepository<WeddingEntity, int>
    {
        Task<WeddingEntity> GetByEmail(string email);
    }
}
