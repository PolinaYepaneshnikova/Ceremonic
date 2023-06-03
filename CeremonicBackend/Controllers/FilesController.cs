using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using CeremonicBackend.Repositories;
using CeremonicBackend.Repositories.Interfaces;

namespace CeremonicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        IFileRepository _fileRepository { get; set; }
        public FilesController(IWebHostEnvironment env)
        {
            _fileRepository = new FileRepository(this, env);
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
    }
}
