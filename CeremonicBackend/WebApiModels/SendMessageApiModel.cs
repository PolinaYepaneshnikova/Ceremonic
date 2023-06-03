using Microsoft.AspNetCore.Http;

namespace CeremonicBackend.WebApiModels
{
    public class SendMessageApiModel
    {
        public string Text { get; set; }

        public IFormFile ImageFile { get; set; }

        public IFormFile File { get; set; }

        public int DestinationUserId { get; set; }
    }
}
