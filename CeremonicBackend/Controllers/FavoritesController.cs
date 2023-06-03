using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


using System.Threading.Tasks;
using System;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;

using Newtonsoft.Json;

using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        IFavoriteService _favoriteService { get; set; }
        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }



        [HttpGet]
        public async Task<ActionResult<List<ProviderApiModel>>> Get()
        {

            try
            {
                string userEmail = User.Claims.ToList().Find(claim => claim.Type == ClaimTypes.Email).Value;
                var providers = await _favoriteService.GetAll(userEmail);

                var jsonSettings = new JsonSerializerSettings { };
                var json = JsonConvert.SerializeObject(providers, Formatting.Indented, jsonSettings);

                return Content(json, "application/json");
            }
            catch (Exception exp)
            {
                return BadRequest(new
                {
                    Error = exp.GetType().Name + ": " + exp.Message + "\n" + exp.StackTrace
                });
            }
        }

        [HttpPost]
        [Route("add/{providerId}")]
        public async Task<IActionResult> Add([FromRoute] int providerId)
        {
            try
            {
                string userEmail = User.Claims.ToList().Find(claim => claim.Type == ClaimTypes.Email).Value;

                await _favoriteService.Add(userEmail, providerId);

                return Ok();
            }
            catch (Exception exp)
            {
                return BadRequest(new
                {
                    Error = exp.GetType().Name + ": " + exp.Message + "\n" + exp.StackTrace
                });
            }
        }

        [HttpDelete]
        [Route("delete/{providerId}")]
        public async Task<IActionResult> Delete([FromRoute] int providerId)
        {
            try
            {
                string userEmail = User.Claims.ToList().Find(claim => claim.Type == ClaimTypes.Email).Value;

                await _favoriteService.Delete(userEmail, providerId);

                return Ok();
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
