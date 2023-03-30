using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

using MongoDB.Driver;

using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.Exceptions;
using CeremonicBackend.WebApiModels;
using CeremonicBackend.Services;

namespace CeremonicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeddingsController : ControllerBase
    {
        IWeddingService _weddingService { get; set; }
        public WeddingsController(IWeddingService weddingService, IWebHostEnvironment env)
        {
            _weddingService = weddingService;

            _weddingService.SetFileRepository(this, env);
        }





        [Authorize]
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromForm] EditWeddingApiModel model)
        {
            try
            {
                string userEmail = User.Claims.ToList().Find(claim => claim.Type == ClaimTypes.Email).Value;

                await _weddingService.Edit(userEmail, model);

                return Ok();
            }
            catch (Exception exp)
            {
                return BadRequest(new
                {
                    Error = exp.Message
                });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("editAvatar/{isMyAvatar}")]
        public async Task<IActionResult> EditAvatar([FromRoute] bool isMyAvatar, [FromForm] EditAvatarApiModel model)
        {
            try
            {
                string userEmail = User.Claims.ToList().Find(claim => claim.Type == ClaimTypes.Email).Value;

                await _weddingService.EditAvatar(userEmail, isMyAvatar, model.AvatarFile);

                return Ok();
            }
            catch (Exception exp)
            {
                return BadRequest(new
                {
                    Error = exp.Message
                });
            }
        }



        [Authorize]
        [HttpGet]
        [Route("currentWedding")]
        public async Task<ActionResult<WeddingApiModel>> CurrentWedding()
        {
            try
            {
                string userEmail = User.Claims.ToList().Find(claim => claim.Type == ClaimTypes.Email).Value;

                return await _weddingService.Get(userEmail);
            }
            catch (NotFoundAppException)
            {
                return BadRequest(new
                {
                    Error = "Wedding not exists"
                });
            }
            catch (Exception exp)
            {
                return BadRequest(new
                {
                    Error = exp.Message
                });
            }
        }
    }
}
