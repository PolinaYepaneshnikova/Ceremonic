using CeremonicBackend.DB.Mongo;
using CeremonicBackend.Repositories.Interfaces;

namespace CeremonicBackend.Repositories
{
    public class WeddingRepository : BaseMongoRepositoryWithUserIdKey<WeddingEntity>, IWeddingRepository
    {
        public const string ConstCollectionName = "weddings";

        public WeddingRepository(ICeremonicMongoDbContext db, IUnitOfWork uow) : base(db, uow, ConstCollectionName) { }
    }
}
