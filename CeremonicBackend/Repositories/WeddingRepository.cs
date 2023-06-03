using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Repositories.Interfaces;
using System.Threading.Tasks;

namespace CeremonicBackend.Repositories
{
    public class WeddingRepository : BaseMongoRepositoryWithUserIdKey<WeddingEntity>, IWeddingRepository
    {
        public const string ConstCollectionName = "weddings";

        public WeddingRepository(ICeremonicMongoDbContext db, IUnitOfWork uow) : base(db, uow, ConstCollectionName) { }

        public async Task<WeddingEntity> GetByEmail(string email)
        {
            UserEntity user = await _UoW.UserRepository.GetByEmail(email);

            if (user is null)
            {
                throw new NotFoundAppException($"user not found");
            }

            WeddingEntity wedding = await _UoW.WeddingRepository.GetById(user.Id);

            return wedding;
        }
    }
}
