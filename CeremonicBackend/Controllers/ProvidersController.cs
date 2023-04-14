using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

using System.Collections.Generic;
using System.Threading.Tasks;
using System;

using CeremonicBackend.WebApiModels;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.Exceptions;
using System.Linq;

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



        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProviderApiModel>> Get([FromRoute] int id)
        {
            try
            {
                return Ok(await _providerService.Get(id));
            }
            catch (Exception exp)
            {
                return BadRequest(new
                {
                    Error = exp.GetType().Name + ": " + exp.Message + "\n" + exp.StackTrace
                });
            }
        }



        [HttpGet]
        [Route("sortOptions")]
        public ActionResult<List<string>> SortOptions()
        {
            try
            {
                return OrderProvidersBy.Values.ToList();
            }
            catch (Exception exp)
            {
                return BadRequest(new
                {
                    Error = exp.GetType().Name + ": " + exp.Message + "\n" + exp.StackTrace
                });
            }
        }

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
                    Error = exp.GetType().Name + ": " + exp.Message + "\n" + exp.StackTrace
                });
            }
        }
    }
}
