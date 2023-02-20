using System;
using System.Security.Claims;
using System.Threading.Tasks;

using CeremonicBackend.Authentification;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Services
{
    public class EmailAccountService : AccountService
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public EmailAccountService(IUnitOfWork uow) : base(uow) { }

        public override async Task ValidateInputOrThrowExeption()
        {
            await Task.Run(() => { });

            if (string.IsNullOrEmpty(Email))
            {
                throw new ArgumentException($"{nameof(Email)}");
            }
            if (string.IsNullOrEmpty(Password))
            {
                throw new ArgumentException($"{nameof(Password)}");
            }
        }

        public override async Task<bool> DoesUserExist()
            => await _UoW.UserRepository.GetByEmail(Email) is not null;

        public override async Task AuthenticateUserOrThrowExeption()
        {
            UserEntity user = await _UoW.UserRepository.GetByEmail(Email);

            string inputHashPass = HashPassword(Password);
            string userHashPass = await _UoW.UserRepository.GetHashPasswordById(user.Id);
            if (userHashPass != inputHashPass)
            {
                throw new NotFoundAppException($"uncorrect password");
            }
        }

        public override JwtApiModel GenerateJwt()
        {
            ClaimsIdentity claims = GetIdentity(new {
                Email,
                Role = ReturnedValueFromUserCreatorService.Role,
            });
            string jwtString = JwtTokenizer.GetEncodedJWT(claims, AuthOptions.Lifetime);

            return new JwtApiModel(jwtString);
        }
    }
}
