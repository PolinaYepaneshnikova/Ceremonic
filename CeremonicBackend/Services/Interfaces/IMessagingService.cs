using CeremonicBackend.WebApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IMessagingService
    {
        void SetProperties(ControllerBase controller, IWebHostEnvironment env);

        Task<List<MessagingCardApiModel>> GetMessagingCards(string userEmail);
        Task<MessagingApiModel> GetMessaging(string user1Email, int user2Id);
        Task<string> GetContactNameById(string userEmail);
        Task SendMessage(string authorEmail, SendMessageApiModel message, DateTime postedAt);
        Task<List<MessageApiModel>> GetNewMessages(string user1Email, int user2Id);
        Task MessagesIsViewed(string user1Email, int user2Id, List<int> messageIDs);
    }
}
