using MongoDB.Driver;

namespace CeremonicBackend.DB.Mongo
{
    public interface ICeremonicMongoDbContext
    {
        public IMongoDatabase Database { get; }

        public IMongoCollection<WeddingEntity> Weddings { get; }

        public IMongoCollection<ServiceEntity> Services { get; }

        public void CreateIndexes();
    }
}
