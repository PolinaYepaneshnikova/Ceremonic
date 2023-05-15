using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Repositories;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CeremonicBackend.Mappings;
using System.Linq;

namespace CeremonicBackend.Services
{
    public class MessagingService : IMessagingService
    {
        protected IUnitOfWork _UoW { get; set; }
        public MessagingService(IUnitOfWork uow)
        {
            _UoW = uow;
        }

        protected FileRepository _fileRepository { get; set; }
        public void SetProperties(ControllerBase controller, IWebHostEnvironment env)
        {
            _fileRepository = new FileRepository(controller, env);
        }

        public async Task<List<MessagingCardApiModel>> GetMessagingCards(string userEmail)
        {
            UserEntity user = await _UoW.UserRepository.GetByEmail(userEmail);

            return await _UoW.MessagingRepository.GetMessagingCardsOfCompanions(user.Id);
        }

        public async Task<MessagingApiModel> GetMessaging(string user1Email, int user2Id)
        {
            UserEntity user1 = await _UoW.UserRepository.GetByEmail(user1Email),
                       user2 = await _UoW.UserRepository.GetById(user2Id);

            return await (await _UoW.MessagingRepository.GetByUsersId(user1.Id, user2.Id))
                .ToMessagingApiModel();
        }

        public async Task SendMessage(string authorEmail, SendMessageApiModel message, DateTime postedAt)
        {
            UserEntity user1 = await _UoW.UserRepository.GetByEmail(authorEmail),
                       user2 = await _UoW.UserRepository.GetById(message.DestinationUserId);

            MessageEntity entity = await message.ToMessageEntity(authorEmail, postedAt, _UoW.UserRepository);

            if (message.ImageFile is not null)
            {
                entity.ImageFileName = await _fileRepository.Add("Images", message.ImageFile);
            }
            if (message.File is not null)
            {
                entity.FileName = await _fileRepository.Add("Files", message.File);
            }

            await _UoW.MessagingRepository.Add(user1.Id, user2.Id, entity);
        }

        public async Task<List<MessageApiModel>> GetNewMessages(string user1Email, int user2Id)
        {
            UserEntity user1 = await _UoW.UserRepository.GetByEmail(user1Email),
                       user2 = await _UoW.UserRepository.GetById(user2Id);

            return (await _UoW.MessagingRepository.GetNewMessagesInMessaging(user1.Id, user2.Id))
                .Select(m => m.ToMessageApiModel()).ToList();
        }

        public async Task MessagesIsViewed(string user1Email, int user2Id, List<int> messageIDs)
        {
            UserEntity user1 = await _UoW.UserRepository.GetByEmail(user1Email),
                       user2 = await _UoW.UserRepository.GetById(user2Id);

            foreach (int idMessage in messageIDs)
            {
                await _UoW.MessagingRepository.SetViewed(user1.Id, user2.Id, idMessage);
            }
        }
    }
}
