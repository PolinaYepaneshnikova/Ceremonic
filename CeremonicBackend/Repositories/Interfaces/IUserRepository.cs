using System.Threading.Tasks;

using CeremonicBackend.DB.Relational;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserEntity, int>
    {
        Task<UserEntity> GetByEmail(string email);
        Task<string> GetEmailById(int id);
        Task<string> GetHashPasswordById(int id);
    }
}
