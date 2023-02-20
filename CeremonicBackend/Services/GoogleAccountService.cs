using System.Security.Claims;
using System;
using System.Threading.Tasks;

using Google.Apis.Auth;
using static Google.Apis.Auth.GoogleJsonWebSignature;

using CeremonicBackend.Authentification;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Services
{
    public class GoogleAccountService : AccountService
    {
        public string TokenId { get; set; }
        protected Payload Payload { get; set; }

        public GoogleAccountService(IUnitOfWork uow) : base(uow) { }

        public override async Task ValidateInputOrThrowExeption()
        {
            await Task.Run(() => { });

            if (string.IsNullOrEmpty(TokenId))
            {
                throw new ArgumentException($"{nameof(TokenId)}");
            }
        }

        public override async Task<bool> DoesUserExist()
        {
            Payload =
                await GoogleJsonWebSignature.ValidateAsync(
                    TokenId,
                    new GoogleJsonWebSignature.ValidationSettings()
                );

            UserCreatorService.Model.Email = Payload.Email;

            UserEntity user = await _UoW.UserRepository.GetByEmail(Payload.Email);

            return user is not null;
        }

        public override async Task AuthenticateUserOrThrowExeption()
            => await Task.Run(() => { });

        public override JwtApiModel GenerateJwt()
        {
            ClaimsIdentity claims = GetIdentity(new
            {
                Payload.Email,
                ReturnedValueFromUserCreatorService.Role,
            });
            string jwtString = JwtTokenizer.GetEncodedJWT(claims, AuthOptions.Lifetime);

            return new JwtApiModel(jwtString);
        }
    }
}
