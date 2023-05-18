using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Security.Claims;

using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;
using CeremonicBackend.SignalRHubs;

namespace CeremonicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessengerController : ControllerBase
    {
        IHubContext<MessengerHub> _messengerContext { get; set; }
        IMessagingService _messagingService { get; set; }
        IUserService _userService { get; set; }
        public MessengerController(
            IHubContext<MessengerHub> messengerContext,
            IMessagingService messagingService,
            IUserService userService,
            IWebHostEnvironment env
        )
        {
            _messengerContext = messengerContext;
            _messagingService = messagingService;
            _userService = userService;
            _messagingService.SetProperties(this, env);
        }



        [HttpPost]
        [Route("sendMessage")]
        public async Task<IActionResult> SendMessage([FromForm] SendMessageApiModel message)
        {
            string userEmail = User.Claims.ToList().Find(claim => claim.Type == ClaimTypes.Email).Value;
            string destinationEmail = await _userService.GetEmailById(message.DestinationUserId);

            await _messagingService.SendMessage(userEmail, message, DateTime.Now);
            IEnumerable<MessagingCardApiModel> messagingCards;

            if (userEmail != destinationEmail)
            {
                messagingCards = await _messagingService.GetMessagingCards(userEmail);
                await _messengerContext.Clients.User(userEmail).SendAsync("MessagingsIsUpdated", messagingCards);
            }

            messagingCards = await _messagingService.GetMessagingCards(destinationEmail);
            await _messengerContext.Clients.User(destinationEmail).SendAsync("MessagingsIsUpdated", messagingCards);

            return Ok();
        }

        [HttpGet]
        [Route("messagingCards")]
        public async Task<ActionResult<List<MessagingCardApiModel>>> MessagingCards()
        {
            string userEmail = User.Claims.ToList().Find(claim => claim.Type == ClaimTypes.Email).Value;

            List<MessagingCardApiModel> messagingCards = await _messagingService.GetMessagingCards(userEmail);

            return messagingCards;
        }

        [HttpGet]
        [Route("messaging/{companionId}")]
        public async Task<ActionResult<MessagingApiModel>> Messaging([FromRoute] int companionId)
        {
            string userEmail = User.Claims.ToList().Find(claim => claim.Type == ClaimTypes.Email).Value;

            MessagingApiModel messaging = await _messagingService.GetMessaging(userEmail, companionId);

            return messaging;
        }

        [HttpGet]
        [Route("newMessages/{companionId}")]
        public async Task<ActionResult<List<MessageApiModel>>> NewMessages([FromRoute] int companionId)
        {
            string userEmail = User.Claims.ToList().Find(claim => claim.Type == ClaimTypes.Email).Value;

            List<MessageApiModel> messages
                = await _messagingService.GetNewMessages(userEmail, companionId);

            return messages;
        }

        [HttpPost]
        [Route("messagesIsViewed/{companionId}")]
        public async Task<IActionResult> MessagesIsViewed([FromRoute] int companionId, [FromBody] List<int> messageIDs)
        {
            string userEmail = User.Claims.ToList().Find(claim => claim.Type == ClaimTypes.Email).Value;
            string companionEmail = await _userService.GetEmailById(companionId);

            await _messagingService.MessagesIsViewed(userEmail, companionId, messageIDs);

            await _messengerContext.Clients.User(userEmail).SendAsync("MessagesIsViewed", companionEmail, messageIDs);
            await _messengerContext.Clients.User(companionEmail).SendAsync("MessagesIsViewed", userEmail, messageIDs);

            return Ok();
        }
    }
}
