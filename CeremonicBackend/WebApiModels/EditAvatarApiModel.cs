using Microsoft.AspNetCore.Http;

namespace CeremonicBackend.WebApiModels
{
    public class EditAvatarApiModel
    {
        public IFormFile AvatarFile { get; set; }
    }
}
