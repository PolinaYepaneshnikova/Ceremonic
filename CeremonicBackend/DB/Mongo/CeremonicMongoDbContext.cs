using MongoDB.Driver;

namespace CeremonicBackend.DB.Mongo
{
    public class CeremonicMongoDbContext : ICeremonicMongoDbContext
    {
        string _connectionString;
        public IMongoDatabase Database { get; private set; }

        public IMongoCollection<WeddingEntity> Weddings
        {
            get => Database.GetCollection<WeddingEntity>("weddings");
        }

        public IMongoCollection<ServiceEntity> Services
        {
            get => Database.GetCollection<ServiceEntity>("services");
        }

        public CeremonicMongoDbContext(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            Database = new MongoClient(_connectionString).GetDatabase(databaseName);
        }

        public async void CreateIndexes()
        {
            await Weddings.Indexes
                .CreateOneAsync(new CreateIndexModel<WeddingEntity>(Builders<WeddingEntity>.IndexKeys.Ascending(_ => _.UserId)))
                .ConfigureAwait(false);

            await Services.Indexes
                .CreateOneAsync(new CreateIndexModel<ServiceEntity>(Builders<ServiceEntity>.IndexKeys.Ascending(_ => _.Id)))
                .ConfigureAwait(false);
        }
    }
}
