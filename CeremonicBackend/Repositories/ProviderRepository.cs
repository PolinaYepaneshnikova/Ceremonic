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
using CeremonicBackend.WebApiModels;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

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

        public async Task<List<ProviderEntity>> Search(SearchProviderApiModel model)
        {
            var filter = Builders<BsonDocument>.Filter.Empty;

            // Фильтр по ключевым словам
            if (!string.IsNullOrEmpty(model.keywords))
            {
                var keywordsFilters = new List<FilterDefinition<BsonDocument>>();
                string[] keywords = model.keywords.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var keyword in keywords)
                {
                    var keywordFilter =
                        Builders<BsonDocument>.Filter.Regex("BrandName", new BsonRegularExpression(keyword, "i"))
                        |
                        Builders<BsonDocument>.Filter.Regex("City", new BsonRegularExpression(keyword, "i"))
                        |
                        Builders<BsonDocument>.Filter.Regex("Info", new BsonRegularExpression(keyword, "i"))
                        |
                        Builders<BsonDocument>.Filter.Regex("PlaceName", new BsonRegularExpression(keyword, "i"));

                    keywordsFilters.Add(keywordFilter);
                }

                filter &= Builders<BsonDocument>.Filter.Or(keywordsFilters);
            }

            // Фильтр по списку названий услуг
            if (model.serviceNames is not null && model.serviceNames.Any())
            {
                IEnumerable<int> serviceIds = model.serviceNames
                    .Select(async serviceName => await _UoW.ServiceRepository.GetByName(serviceName))
                    .Select(s => s.Result)
                    .Select(s => s.Id);

                filter &= Builders<BsonDocument>.Filter.In("ServiceId", serviceIds);
            }

            // Фильтр по городу
            if (!string.IsNullOrEmpty(model.city))
            {
                filter &= Builders<BsonDocument>.Filter.Eq("City", model.city);
            }

            // Фильтр по категории цены
            if (model.numberOfPriceCategory is int numberOfPCategory)
            {
                IEnumerable<GeneralServiceEntity> services = await _UoW.ServiceRepository.GetByPredicate(s => true);
                var priceFilters = new List<FilterDefinition<BsonDocument>>();

                foreach (GeneralServiceEntity service in services)
                {
                    priceFilters.Add(
                        Builders<BsonDocument>.Filter.Eq("ServiceId", service.Id)
                        &
                        Builders<BsonDocument>.Filter.Eq(
                            "AveragePrice.Min",
                            service.MongoServiceEntity.PriceRanges[numberOfPCategory].Min
                        )
                        //&
                        //Builders<BsonDocument>.Filter.Eq(
                        //    "AveragePrice.Max",
                        //    service.MongoServiceEntity.PriceRanges[numberOfPCategory].Max
                        //)
                    );
                }

                filter &= Builders<BsonDocument>.Filter.Or(priceFilters);
            }

            // Фильтр по количеству гостей
            if (model.numberOfGuestCountCategory is int numberOfGCategory)
            {
                IEnumerable<GeneralServiceEntity> services = await _UoW.ServiceRepository
                    .GetByPredicate(s => s.MongoServiceEntity.GuestCountRanges is not null);

                var priceFilters = new List<FilterDefinition<BsonDocument>>();

                foreach (GeneralServiceEntity service in services)
                {
                    priceFilters.Add(
                        Builders<BsonDocument>.Filter.Eq("ServiceId", service.Id)
                        &
                        Builders<BsonDocument>.Filter.Eq(
                            "GuestCount.Min",
                            service.MongoServiceEntity.GuestCountRanges[numberOfGCategory].Min
                        )
                        //&
                        //Builders<BsonDocument>.Filter.Eq(
                        //    "GuestCount.Max",
                        //    service.MongoServiceEntity.GuestCountRanges[numberOfGCategory].Max
                        //)
                    );
                }

                filter &= Builders<BsonDocument>.Filter.Or(priceFilters);
            }

            IEnumerable<BsonDocument> bsonDocuments = await _db.Database.GetCollection<BsonDocument>(CollectionName)
                .Find(filter)
                .ToListAsync();

            // Преобразуем найденные документы в сущности
            IEnumerable<ProviderEntity> providers = bsonDocuments.Select(async d =>
            {
                int serviceId = d.GetValue("ServiceId").AsInt32;
                string serviceName = (await _UoW.ServiceRepository.GetById(serviceId))
                    .RelationalServiceEntity.Name;

                if (serviceName == "Банкетна зала" || serviceName == "Місце проведення церемонії")
                {
                    return BsonSerializer.Deserialize<PlaceProviderEntity>(d);
                }
                else
                {
                    return BsonSerializer.Deserialize<ProviderEntity>(d);
                }
            }).Select(t => t.Result);

            return providers.ToList();
        }
    }
}