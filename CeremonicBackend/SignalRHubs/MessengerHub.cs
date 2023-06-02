using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;

using SignalRSwaggerGen.Attributes;

using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.Services;

namespace CeremonicBackend.SignalRHubs
{
    [SignalRHub]
    public class MessengerHub : Hub
    {
        public MessengerHub()
        {
            
        }
    }
}
