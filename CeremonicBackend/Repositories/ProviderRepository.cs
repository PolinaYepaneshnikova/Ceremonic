using CeremonicBackend.DB.Mongo;
using CeremonicBackend.Repositories.Interfaces;

namespace CeremonicBackend.Repositories
{
    public class ProviderRepository : BaseMongoRepositoryWithUserIdKey<ProviderEntity>, IProviderRepository
    {
        public const string ConstCollectionName = "providers";

        public ProviderRepository(ICeremonicMongoDbContext db) : base(db, ConstCollectionName) { }
    }
}
