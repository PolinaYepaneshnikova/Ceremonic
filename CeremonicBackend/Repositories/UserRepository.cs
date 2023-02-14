using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MongoDB.Driver.Linq;

using CeremonicBackend.DB.Relational;
using CeremonicBackend.WebApiModels;
using CeremonicBackend.Repositories.Interfaces;

namespace CeremonicBackend.Repositories
{
    public class UserRepository : BaseRelationalRepository<UserEntity, int>, IUserRepository
    {
        public UserRepository(CeremonicRelationalDbContext db) : base(db) { }

        public async Task<UserEntity> GetByEmail(string email)
            => await Task.Run(() =>_db.Users.Include(e => e.LoginInfo)
            .Where(e => e.LoginInfo.Email == email).FirstOrDefault());

        public async Task<string> GetHashPasswordById(int id)
            => await Task.Run(() => _db.UserLoginInfos
            .Where(e => e.Id == id).FirstOrDefault()
            .PasswordHash);
    }
}
