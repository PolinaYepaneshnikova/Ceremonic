using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IWeddingService
    {
        Task<WeddingEntity> CreateForUser(UserEntity user);
    }
}
