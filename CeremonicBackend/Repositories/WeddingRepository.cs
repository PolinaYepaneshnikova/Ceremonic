using System;
using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.DB.Relational;

namespace CeremonicBackend.Repositories
{
    public class WeddingRepository : BaseMongoRepositoryWithUserIdKey<WeddingEntity>, IWeddingRepository
    {
        public const string ConstCollectionName = "weddings";

        public WeddingRepository(ICeremonicMongoDbContext db) : base(db, ConstCollectionName) { }
    }
}
