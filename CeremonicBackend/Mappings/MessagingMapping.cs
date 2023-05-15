using System;
using System.Linq;
using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Mappings
{
    public static class MessagingMapping
    {
        public static async Task<MessagingCardApiModel> ToMessagingCardApiModel(
            this MessagingEntity entity,
            int CompanionId,
            IWeddingRepository weddingRepository,
            IProviderRepository providerRepository
        )
        {
            var messagingCard = new MessagingCardApiModel()
            {
                UserName = await ContactNameById(CompanionId, weddingRepository, providerRepository),
                CountOfNotViewedMessages = entity.MessagesList.Sum(m => (m.NotViewed ?? false) ? 1 : 0)
            };

            return messagingCard;
        }

        public static async Task<MessagingApiModel> ToMessagingApiModel(this MessagingEntity entity)
        {
            await Task.Run(() => { });

            return new MessagingApiModel()
            {
                User1Id = entity.User1Id,
                User2Id = entity.User2Id,
                MessagesList = entity.MessagesList.ToList()
                   .Select(m => m.ToMessageApiModel()).ToList(),
            };
        }

        public static MessageApiModel ToMessageApiModel(this MessageEntity entity)
           => new MessageApiModel()
           {
               Id = entity.Id,
               AuthorId = entity.AuthorId,
               Text = entity.Text,
               ImageFileName = entity.ImageFileName,
               FileName = entity.FileName,
               PostedAt = entity.PostedAt,
               NotViewed = entity.NotViewed ?? false,
           };

        public static async Task<MessageEntity> ToMessageEntity(this SendMessageApiModel model, IUserRepository userRepository, string userEmail, DateTime postedAt)
           => new MessageEntity()
           {
               Id = 0,
               AuthorId = (await userRepository.GetByEmail(userEmail)).Id,
               Text = model.Text,
               ImageFileName = model.ImageFile?.FileName,
               FileName = model.File?.FileName,
               PostedAt = postedAt,
               NotViewed = true,
           };

        public static MessagingEntity OrderByMyAndCompanion(this MessagingEntity messaging, int myId, int companionId)
        {
            bool haveToRevert = messaging.User1Id != myId;

            if (haveToRevert)
            {
                return new MessagingEntity
                {
                    User1Id = myId,
                    User2Id = companionId,
                    MessagesList = messaging.MessagesList.ToList(),
                };
            }

            return messaging;
        }

        public static async Task<string> ContactNameById(
            int userId,
            IWeddingRepository weddingRepository,
            IProviderRepository providerRepository
        )
        {
            WeddingEntity wedding = await weddingRepository.GetById(userId);
            ProviderEntity provider = await providerRepository.GetById(userId);

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
