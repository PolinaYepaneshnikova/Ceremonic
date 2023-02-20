using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IWeddingRepository : IBaseRepository<WeddingEntity, int>
    {
        Task<WeddingEntity> CreateForUser(UserEntity user);
    }
}
