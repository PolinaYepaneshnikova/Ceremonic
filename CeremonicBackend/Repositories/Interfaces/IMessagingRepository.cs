using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.WebApiModels;
using System.Linq;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IMessagingRepository
    {
        Task<MessagingEntity> GetByUsersId(int id1, int id2);
        Task<List<MessagingCardApiModel>> GetMessagingCardsOfCompanions(int id);
        Task<List<MessageEntity>> GetNewMessagesInMessaging(int id1, int id2);
        Task<string> ContactNameById(int userId);

        Task<MessagingEntity> Add(int id1, int id2, MessageEntity message);
        Task<MessagingEntity> SetViewed(int id1, int id2, int idMessage);

        Task<IQueryable<MessagingEntity>> GetByPredicate(Expression<Func<MessagingEntity, bool>> predicate);
        Task<MessagingEntity> Delete(int id1, int id2);
        Task<MessagingEntity> Delete(int id1, int id2, int idMessage);
    }
}
