using MongoDB.Driver;
using System.Collections.Generic;

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

        public IMongoCollection<ProviderEntity> Providers
        {
            get => Database.GetCollection<ProviderEntity>("providers");
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

            await Providers.Indexes
                .CreateOneAsync(new CreateIndexModel<ProviderEntity>(Builders<ProviderEntity>.IndexKeys.Ascending(_ => _.UserId)))
                .ConfigureAwait(false);

            await Services.Indexes
                .CreateOneAsync(new CreateIndexModel<ServiceEntity>(Builders<ServiceEntity>.IndexKeys.Ascending(_ => _.Id)))
                .ConfigureAwait(false);

            if (Services.CountDocuments(e => true) == 0)
            {
                await Services.InsertManyAsync(new List<ServiceEntity>()
                {
                    new ServiceEntity(){
                       Id = 1,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 1500),
                           new RangeEntity(1500, 3000),
                           new RangeEntity(3000, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 2,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 1500),
                           new RangeEntity(1500, 3000),
                           new RangeEntity(3000, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 3,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 3000),
                           new RangeEntity(3000, 6000),
                           new RangeEntity(6000, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 4,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 20000),
                           new RangeEntity(20000, 40000),
                           new RangeEntity(40000, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 5,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 1500),
                           new RangeEntity(1500, 2000),
                           new RangeEntity(2000, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 6,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 20000),
                           new RangeEntity(20000, 30000),
                           new RangeEntity(30000, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 7,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(),
                           new RangeEntity(),
                           new RangeEntity(),
                       },
                    },
                    new ServiceEntity(){
                       Id = 8,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 2500),
                           new RangeEntity(2500, 5000),
                        new RangeEntity(5000, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 9,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 600),
                           new RangeEntity(600, 1500),
                           new RangeEntity(1500, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 10,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 10000),
                           new RangeEntity(10000, 20000),
                           new RangeEntity(20000, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 11,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 20000),
                           new RangeEntity(20000, 30000),
                           new RangeEntity(30000, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 12,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 15000),
                           new RangeEntity(15000, 25000),
                           new RangeEntity(25000, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 13,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(),
                           new RangeEntity(),
                           new RangeEntity(),
                       },
                    },
                    new ServiceEntity(){
                       Id = 14,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 2000),
                           new RangeEntity(2000, 4000),
                           new RangeEntity(4000, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 15,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 1700),
                           new RangeEntity(1700, 3000),
                           new RangeEntity(3000, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 16,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 1000),
                           new RangeEntity(1000, 3000),
                           new RangeEntity(3000, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 17,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 2000),
                           new RangeEntity(2000, 3500),
                           new RangeEntity(3500, decimal.MaxValue),
                       },
                    },
                    new ServiceEntity(){
                       Id = 18,
                       PriceRanges = new List<RangeEntity>()
                       {
                           new RangeEntity(0, 2500),
                           new RangeEntity(2500, 4000),
                           new RangeEntity(4000, decimal.MaxValue),
                       },
                    },
                });
            }
        }
    }
}

