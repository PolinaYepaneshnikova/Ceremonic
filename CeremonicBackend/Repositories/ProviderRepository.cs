using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Repositories.Interfaces;
using System.Threading.Tasks;

namespace CeremonicBackend.Repositories
{
    public class ProviderRepository : BaseMongoRepositoryWithUserIdKey<ProviderEntity>, IProviderRepository
    {
        public const string ConstCollectionName = "providers";

        public ProviderRepository(ICeremonicMongoDbContext db, IUnitOfWork uow) : base(db, uow, ConstCollectionName) { }

        public async Task<ProviderEntity> GetByEmail(string email)
        {
            UserEntity user = await _UoW.UserRepository.GetByEmail(email);

            if (user is null)
            {
                throw new NotFoundAppException($"user not found");
            }

            ProviderEntity provider = await _UoW.ProviderRepository.GetById(user.Id);

            return provider;
        }
    }
}
