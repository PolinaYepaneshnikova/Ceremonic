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

namespace CeremonicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountService _accountService { get; set; }
        IGoogleAccountService _googleAccountService { get; set; }
        IUserService _userService { get; set; }
        public AccountController(IAccountService accountService, IGoogleAccountService googleAccountService, IUserService userService)
        {
            _accountService = accountService;
            _googleAccountService = googleAccountService;
            _userService = userService;
        }

        /*{
          "firstName": "Павло",
          "lastName": "Дунайський",
          "email": "PavloDunayskyy@net.ua,
          "password": "1234"
        }*/

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<JwtApiModel>> Login([FromBody] LoginApiModel model)
        {
            try
            {
                return await _accountService.Login(model.Email, model.Password);
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

        [HttpPost]
        [Route("registration")]
        public async Task<ActionResult<JwtApiModel>> Registration([FromBody] RegistrationApiModel model)
        {
            try
            {
                return await _accountService.Registration(model);
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

        [HttpPost]
        [Route("googleLogin")]
        public async Task<ActionResult<JwtApiModel>> GoogleLogin([FromBody] TokenIdApiModel model)
        {
            try
            {
                return await _googleAccountService.Login(model.TokenId);
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

        [HttpPost]
        [Route("googleRegistration")]
        public async Task<ActionResult<JwtApiModel>> GoogleRegistration([FromBody] GoogleRegistrationApiModel model)
        {
            try
            {
                return await _googleAccountService.Registration(model);
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



        [Authorize]
        [HttpGet]
        [Route("currentUser")]
        public async Task<ActionResult<UserApiModel>> CurrentUser()
        {
            try
            {
                string userEmail = User.Claims.ToList().Find(claim => claim.Type == "Email").Value;

                return await _userService.GetUserByEmail(userEmail);
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
    }
}
