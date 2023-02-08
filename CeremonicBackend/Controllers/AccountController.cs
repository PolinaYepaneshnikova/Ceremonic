using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public class AccountController : ControllerBase
    {
        IAccountService _accountService { get; set; }
        IUserService _userService { get; set; }
        public AccountController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }



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
        public async Task<ActionResult<UserApiModel>> Registration([FromBody] RegistrationApiModel model)
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
