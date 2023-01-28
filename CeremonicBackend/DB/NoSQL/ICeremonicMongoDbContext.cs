using MongoDB.Driver;

namespace CeremonicBackend.DB.NoSQL
{
    public interface ICeremonicMongoDbContext
    {
        public IMongoDatabase Database { get; }

        public IMongoCollection<WeddingEntity> Weddings { get; }

        public void CreateIndexes();
    }
}
