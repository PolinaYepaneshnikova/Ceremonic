using System.Threading.Tasks;

using Google.Apis.Auth;
using static Google.Apis.Auth.GoogleJsonWebSignature;

using CeremonicBackend.DB.Relational;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Authentification;
using System.Security.Claims;

namespace CeremonicBackend.Services
{
    public class GoogleAccountService : IGoogleAccountService
    {
        protected IUnitOfWork _UoW { get; set; }
        public GoogleAccountService(IUnitOfWork uow)
        {
            _UoW = uow;
        }



        public async Task<JwtApiModel> Login(string tokenId)
        {
            Payload payload =
                await GoogleJsonWebSignature.ValidateAsync(
                    tokenId,
                    new GoogleJsonWebSignature.ValidationSettings()
                );

            UserEntity user = await _UoW.UserRepository.GetByEmail(payload.Email);
            if (user is null)
            {
                throw new NotFoundAppException($"user not found");
            }

            ClaimsIdentity claims = AccountService.GetIdentity(payload.Email, "User");
            string jwtString = JwtTokenizer.GetEncodedJWT(claims, AuthOptions.Lifetime);

            return new JwtApiModel(jwtString);
        }

        public async Task<JwtApiModel> Registration(GoogleRegistrationApiModel dto)
        {
            Payload payload =
                await GoogleJsonWebSignature.ValidateAsync(
                    dto.TokenId,
                    new GoogleJsonWebSignature.ValidationSettings()
                );

            UserEntity user = await _UoW.UserRepository.GetByEmail(payload.Email);

            if (user is not null)
            {
                throw new AlreadyExistAppException($"user already exist");
            }

            user = new UserEntity()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                LoginInfo = new UserLoginInfoEntity()
                {
                    Email = payload.Email,
                    PasswordHash = "GoogleAPI",
                },
            };
            user = await _UoW.UserRepository.Add(user);
            await _UoW.SaveChanges();

            ClaimsIdentity claims = AccountService.GetIdentity(payload.Email, "User");
            string jwtString = JwtTokenizer.GetEncodedJWT(claims, AuthOptions.Lifetime);

            return new JwtApiModel(jwtString);
        }
    }
}
