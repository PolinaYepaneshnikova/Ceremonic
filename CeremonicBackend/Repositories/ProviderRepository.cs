using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;

using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Repositories.Interfaces;
using SharpCompress.Common;
using System;
using Quartz.Impl.AdoJobStore.Common;

namespace CeremonicBackend.Repositories
{
    public class ProviderRepository : BaseMongoRepositoryWithUserIdKey<ProviderEntity>, IProviderRepository
    {
        public const string ConstCollectionName = "providers";

        public ProviderRepository(ICeremonicMongoDbContext db, IUnitOfWork uow) : base(db, uow, ConstCollectionName) { }

        public new async Task<ProviderEntity> GetById(int userId)
        {
            var bsonDocument = await _db.Database.GetCollection<BsonDocument>(CollectionName)
                .FindAsync(Builders<BsonDocument>.Filter.Eq("_id", userId))
                .Result
                .FirstOrDefaultAsync();

            if (bsonDocument is null)
            {
                return null;
            }

            int serviceId = bsonDocument.GetValue("ServiceId").AsInt32;
            string serviceName = (await _UoW.ServiceRepository.GetById(serviceId))
                .RelationalServiceEntity.Name;

            ProviderEntity entity;

            if (serviceName == "Банкетна зала" || serviceName == "Місце проведення церемонії")
            {
                entity = BsonSerializer.Deserialize<PlaceProviderEntity>(bsonDocument);
            }
            else
            {
                entity = BsonSerializer.Deserialize<ProviderEntity>(bsonDocument);
            }

            return entity;
        }

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
