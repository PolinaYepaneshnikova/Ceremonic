using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;
using System.Linq;

using MongoDB.Driver;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.WebApiModels;
using CeremonicBackend.Mappings;

namespace CeremonicBackend.Repositories
{
    public class MessagingRepository : IMessagingRepository
    {
        protected ICeremonicMongoDbContext _db;
        protected IUnitOfWork _UoW;

        public const string ConstCollectionName = "providers";
        public string CollectionName = ConstCollectionName;

        public MessagingRepository(ICeremonicMongoDbContext db, IUnitOfWork uow)
        {
            _db = db;
            _UoW = uow;
        }

        public async Task<MessagingEntity> GetByUsersId(int id1, int id2)
        {
            MessagingEntity messaging = (await _db.Messagings.FindAsync(
                e => e.User1Id == id1 && e.User2Id == id2 || e.User1Id == id2 && e.User2Id == id1
            )).FirstOrDefault();

            if (messaging is null)
            {
                return new MessagingEntity
                {
                    User1Id = id1,
                    User2Id = id2,
                    MessagesList = new List<MessageEntity>(),
                };
            }
            else
            {
                messaging.MessagesList = messaging.MessagesList.ToList();
            }

            return messaging.OrderByMyAndCompanion(id1, id2);
        }

        public async Task<List<MessagingCardApiModel>> GetMessagingCardsOfCompanions(int id)
        {
            List<MessagingEntity> messagings = (await _db.Messagings.FindAsync(
                e => e.User1Id == id || e.User2Id == id
            )).ToList();

            List<MessagingCardApiModel> messagingCards
                = messagings
                .Select(async e => await e.ToMessagingCardApiModel(e.User1Id == id ? e.User2Id : e.User1Id, this))
                .Select(e => e.Result)
                .ToList();


            return messagingCards;
        }

        public async Task<List<MessageEntity>> GetNewMessagesInMessaging(int id1, int id2)
        {
            MessagingEntity messaging = await GetByUsersId(id1, id2);

            List<MessageEntity> messages = messaging.MessagesList.ToList().Where(e => e.NotViewed ?? false).ToList();

            return messages;
        }



        public async Task<MessagingEntity> Add(int id1, int id2, MessageEntity message)
        {
            MessagingEntity messaging = await GetByUsersId(id1, id2);

            if (messaging.MessagesList.Count() == 0)
            {
                message.Id = 0;
                messaging.MessagesList = new List<MessageEntity>() { message };

                _db.Messagings.InsertOne(messaging);
            }
            else
            {
                message.Id = messaging.MessagesList.Any() ? messaging.MessagesList.Last().Id + 1 : 0;

                var builder = Builders<MessagingEntity>.Filter;
                var filter =
                    builder.Eq("User1Id", id1) & builder.Eq("User2Id", id2)
                    |
                    builder.Eq("User1Id", id2) & builder.Eq("User2Id", id1);

                _db.Messagings.UpdateOne(
                    filter,
                    Builders<MessagingEntity>.Update.Push("MessagesList", message)
                );
            }

            return messaging;
        }

        public async Task<MessagingEntity> SetViewed(int id1, int id2, int idMessage)
        {
            var messaging = await GetByUsersId(id1, id2);
            var message = messaging.MessagesList.FirstOrDefault(e => e.Id == idMessage);

            message.NotViewed = null;
            messaging.MessagesList[message.Id] = message;

            var builder = Builders<MessagingEntity>.Filter;
            var filter =
                builder.Eq("User1Id", id1) & builder.Eq("User2Id", id2)
                |
                builder.Eq("User1Id", id2) & builder.Eq("User2Id", id1);

            _db.Messagings.UpdateOne(
                    filter,
                    Builders<MessagingEntity>.Update.Set("MessagesList", messaging.MessagesList));

            return messaging;
        }



        public async Task<IQueryable<MessagingEntity>> GetByPredicate(Expression<Func<MessagingEntity, bool>> predicate)
            => (await _db.Messagings.FindAsync(
                predicate ?? (e => true)
            )).ToEnumerable().AsQueryable();

        public async Task<MessagingEntity> Delete(int id1, int id2)
        {
            var messaging = await GetByUsersId(id1, id2);

            _db.Messagings.DeleteOne(
                e =>
                    e.User1Id == messaging.User1Id && e.User2Id == messaging.User2Id
                    ||
                    e.User1Id == messaging.User2Id && e.User2Id == messaging.User1Id
            );

            return messaging;
        }

        public async Task<MessagingEntity> Delete(int id1, int id2, int idMessage)
        {
            var messaging = await GetByUsersId(id1, id2);
            var message = messaging.MessagesList.FirstOrDefault(e => e.Id == idMessage);

            messaging.MessagesList.Remove(message);

            var builder = Builders<MessagingEntity>.Filter;
            var filter =
                builder.Eq("User1Id", id1) & builder.Eq("User2Id", id2)
                |
                builder.Eq("User1Id", id2) & builder.Eq("User2Id", id1);

            _db.Messagings.ReplaceOne(filter, messaging);

            return messaging;
        }

        public async Task<string> ContactNameById(int userId)
        {
            WeddingEntity wedding = await _UoW.WeddingRepository.GetById(userId);
            ProviderEntity provider = await _UoW.ProviderRepository.GetById(userId);

            if (wedding is not null)
            {
                return wedding.User1.FullName;
            }

            if (provider is not null)
            {
                return provider.BrandName;
            }

            throw new ArgumentException("There are not weddings or providers with such ID.");
        }
    }
}
