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
    public class AgreementsController : ControllerBase
    {
        IHubContext<MessengerHub> _messengerContext { get; set; }
        IMessagingService _messagingService { get; set; }
        IAgreementService _agreementService { get; set; }
        IUserService _userService { get; set; }
        public AgreementsController(
            IHubContext<MessengerHub> messengerContext,
            IMessagingService messagingService,
            IAgreementService agreementService,
            IUserService userService
        )
        {
            _messengerContext = messengerContext;
            _messagingService = messagingService;
            _agreementService = agreementService;
            _userService = userService;
        }



        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> Send([FromBody] SendAgreementApiModel agreement)
        {
            string providerEmail = User.Claims.ToList().Find(claim => claim.Type == ClaimTypes.Email).Value;
            string destinationEmail = await _userService.GetEmailById(agreement.ClientId);

            await _agreementService.Create(providerEmail, agreement, DateTime.Now);

            IEnumerable<MessagingCardApiModel> messagingCards;

            messagingCards = await _messagingService.GetMessagingCards(providerEmail);
            await _messengerContext.Clients.User(providerEmail).SendAsync("MessagingsIsUpdated", messagingCards);

            messagingCards = await _messagingService.GetMessagingCards(destinationEmail);
            await _messengerContext.Clients.User(destinationEmail).SendAsync("MessagingsIsUpdated", messagingCards);

            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AgreementApiModel>> Get([FromRoute] int id)
        {
            AgreementApiModel agreement = await _agreementService.Get(id);

            return agreement;
        }


        [HttpPut]
        [Route("confirm/{id}")]
        public async Task<ActionResult<AgreementApiModel>> Confirm([FromRoute] int id)
        {
            AgreementApiModel agreement = await _agreementService.Confirm(id);

            return agreement;
        }


        [HttpPut]
        [Route("cancel/{id}")]
        public async Task<ActionResult<AgreementApiModel>> Cancel([FromRoute] int id)
        {
            AgreementApiModel agreement = await _agreementService.Confirm(id);

            return agreement;
        }
    }
}
