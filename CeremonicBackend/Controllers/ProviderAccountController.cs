﻿using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System;

using CeremonicBackend.WebApiModels;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Services;
using CeremonicBackend.Mappings;

namespace CeremonicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderAccountController : ControllerBase
    {
        IAccountService _accountService { get; set; }
        EmailAccountService _emailAccountService { get; set; }
        GoogleAccountService _googleAccountService { get; set; }
        ProviderCreatorService _providerCreatorService { get; set; }
        public ProviderAccountController(
            EmailAccountService emailAccountService,
            GoogleAccountService googleAccountService,
            ProviderCreatorService providerCreatorService
        )
        {
            _emailAccountService = emailAccountService;
            _googleAccountService = googleAccountService;
            _providerCreatorService = providerCreatorService;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<ActionResult<JwtApiModel>> EmailRegistration([FromBody] ProviderRegistrationApiModel model)
        {
            _emailAccountService.Email = model.UserRegistrationModel.Email;
            _emailAccountService.Password = model.UserRegistrationModel.Password;

            _accountService = _emailAccountService;

            _providerCreatorService.RegistrationModel = model.UserRegistrationModel;
            _providerCreatorService.ProviderInfo = model.ProviderInfo;
            _accountService.UserCreatorService = _providerCreatorService;

            return await Registration();
        }

        [HttpPost]
        [Route("googleRegistration")]
        public async Task<ActionResult<JwtApiModel>> GoogleRegistration([FromBody] ProviderGoogleRegistrationApiModel model)
        {
            _googleAccountService.TokenId = model.UserRegistrationModel.TokenId;

            _accountService = _googleAccountService;

            _providerCreatorService.RegistrationModel = model.UserRegistrationModel.ToRegistrationApiModel();
            _providerCreatorService.ProviderInfo = model.ProviderInfo;
            _accountService.UserCreatorService = _providerCreatorService;

            return await Registration();
        }





        private async Task<ActionResult<JwtApiModel>> Registration()
        {
            try
            {
                return await _accountService.Registration();
            }
            catch (AlreadyExistAppException)
            {
                return BadRequest(new
                {
                    Error = "User already exist"
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
