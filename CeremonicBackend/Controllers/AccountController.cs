using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System.Linq;
using System;

using CeremonicBackend.WebApiModels;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Services;
using CeremonicBackend.Mappings;
using System.Security.Claims;

namespace CeremonicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IUserService _userService { get; set; }
        IAccountService _accountService { get; set; }
        EmailAccountService _emailAccountService { get; set; }
        GoogleAccountService _googleAccountService { get; set; }
        ClientCreatorService _clientCreatorService { get; set; }
        public AccountController(
            IUserService userService,
            EmailAccountService emailAccountService,
            GoogleAccountService googleAccountService,
            ClientCreatorService clientCreatorService
            )
        {
            _userService = userService;
            _emailAccountService = emailAccountService;
            _googleAccountService = googleAccountService;
            _clientCreatorService = clientCreatorService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<JwtApiModel>> EmailLogin([FromBody] LoginApiModel model)
        {
            _emailAccountService.Email = model.Email;
            _emailAccountService.Password = model.Password;

            _accountService = _emailAccountService;
            _accountService.UserCreatorService = _clientCreatorService;

            return await Login();
        }

        [HttpPost]
        [Route("registration")]
        public async Task<ActionResult<JwtApiModel>> EmailRegistration([FromBody] RegistrationApiModel model)
        {
            _emailAccountService.Email = model.Email;
            _emailAccountService.Password = model.Password;

            _accountService = _emailAccountService;

            _clientCreatorService.RegistrationModel = model;
            _accountService.UserCreatorService = _clientCreatorService;

            return await Registration();
        }

        [HttpPost]
        [Route("googleLogin")]
        public async Task<ActionResult<JwtApiModel>> GoogleLogin([FromBody] TokenIdApiModel model)
        {
            _googleAccountService.TokenId = model.TokenId;

            _accountService = _googleAccountService;

            _clientCreatorService.RegistrationModel = new RegistrationApiModel();
            _accountService.UserCreatorService = _clientCreatorService;

            return await Login();
        }

        [HttpPost]
        [Route("googleRegistration")]
        public async Task<ActionResult<JwtApiModel>> GoogleRegistration([FromBody] GoogleRegistrationApiModel model)
        {
            _googleAccountService.TokenId = model.TokenId;

            _accountService = _googleAccountService;

            _clientCreatorService.RegistrationModel = model.ToRegistrationApiModel();
            _accountService.UserCreatorService = _clientCreatorService;

            return await Registration();
        }



        [Authorize]
        [HttpGet]
        [Route("currentUser")]
        public async Task<ActionResult<UserApiModel>> CurrentUser()
        {
            try
            {
                string userEmail = User.Claims.ToList().Find(claim => claim.Type == ClaimTypes.Email).Value;

                return await _userService.Get(userEmail);
            }
            catch (NotFoundAppException)
            {
                return BadRequest(new
                {
                    Error = "User not exists"
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





        private async Task<ActionResult<JwtApiModel>> Login()
        {
            try
            {
                return await _accountService.Login();
            }
            catch (NotFoundAppException)
            {
                return BadRequest(new
                {
                    Error = "User not exists"
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
