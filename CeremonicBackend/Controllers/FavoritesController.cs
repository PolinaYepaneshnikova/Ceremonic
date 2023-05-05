﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System;
using System.Linq;
using System.Security.Claims;

using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using CeremonicBackend.Mappings;
using CeremonicBackend.Services;
using Newtonsoft.Json;

namespace CeremonicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        IFavoriteService _favoriteService { get; set; }
        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }



        [Authorize]
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
