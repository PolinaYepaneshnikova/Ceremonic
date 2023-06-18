using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using CeremonicBackend.Repositories;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        IFileRepository _fileRepository { get; set; }
        IUserService _userService { get; set; }
        IWeddingService _weddingService { get; set; }
        IProviderService _providerService { get; set; }
        public FilesController(
            IWebHostEnvironment env, 
            IUserService userService, 
            IWeddingService weddingService, 
            IProviderService providerService
        )
        {
            _fileRepository = new FileRepository(this, env);
            _userService = userService;
            _weddingService = weddingService;
            _providerService = providerService;
        }

        [HttpGet]
        [Route("file/{fileName}")]
        public async Task<ActionResult> File(string fileName)
            => await _fileRepository.GetByName("Files", fileName);

        [HttpGet]
        [Route("image/{fileName}")]
        public async Task<ActionResult> Image(string fileName)
            => await _fileRepository.GetByName("Images", fileName);

        [HttpGet]
        [Route("avatar/{fileName}")]
        public async Task<ActionResult> Avatar(string fileName)
            => await _fileRepository.GetByName("Avatars", fileName);

        [HttpGet]
        [Route("avatarById/{userId}")]
        public async Task<ActionResult> Avatar(int userId)
        {
            string email = await _userService.GetEmailById(userId);

            if (await _userService.GetRoleByEmail(email) == "User")
            {
                WeddingApiModel wedding = await _weddingService.Get(userId);

                return await _fileRepository.GetByName("Avatars", wedding.Avatar1FileName);
            }
            else
            {
                ProviderApiModel provider = await _providerService.Get(userId);

                return await _fileRepository.GetByName("Avatars", provider.AvatarFileName);
            }
        }
    }
}
