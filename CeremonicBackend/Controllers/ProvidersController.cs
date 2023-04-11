using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

using System.Collections.Generic;
using System.Threading.Tasks;
using System;

using CeremonicBackend.WebApiModels;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.Exceptions;

namespace CeremonicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        IProviderService _providerService { get; set; }
        public ProvidersController(
            IProviderService providerService,
            IWebHostEnvironment env
        )
        {
            _providerService = providerService;
            _providerService.SetFileRepository(this, env);
        }



        [Authorize]
        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<List<ProviderApiModel>>> Search([FromQuery] SearchProviderApiModel model)
        {
            try
            {
                return Ok(await _providerService.Search(model));
            }
            catch (NotFoundAppException)
            {
                return BadRequest(new
                {
                    Error = "Provider not exists"
                });
            }
            catch (Exception exp)
            {
                return BadRequest(new
                {
                    Error = exp.Message + "\n" + exp.StackTrace
                });
            }
        }
    }
}
