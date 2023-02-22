using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;

using CeremonicBackend.WebApiModels;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Authentification;
using CeremonicBackend.Exceptions;

namespace CeremonicBackend.Services.Interfaces
{
    public abstract class AccountService : IAccountService
    {
        public IUserCreatorService UserCreatorService { get; set; }

        protected dynamic UserAdditionalInfo { get; set; }

        protected IUnitOfWork _UoW { get; set; }
        public AccountService(IUnitOfWork uow)
        {
            _UoW = uow;
        }



        public virtual async Task<JwtApiModel> Login()
        {
            await ValidateInputOrThrowExeption();

            if (!await DoesUserExist())
            {
                throw new NotFoundAppException($"user not found");
            }

            await AuthenticateUserOrThrowExeption();

            return GenerateJwt();
        }

        public virtual async Task<JwtApiModel> Registration()
        {
            await ValidateInputOrThrowExeption();

            if (await DoesUserExist())
            {
                throw new AlreadyExistAppException($"user already exist");
            }

            UserAdditionalInfo = await UserCreatorService.Create();

            return GenerateJwt();
        }



        public abstract Task ValidateInputOrThrowExeption();
        public abstract Task<bool> DoesUserExist();
        public abstract Task AuthenticateUserOrThrowExeption();
        public abstract JwtApiModel GenerateJwt();



        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashPass = sha.ComputeHash(asByteArray);

            return Convert.ToBase64String(hashPass);
        }

        public virtual ClaimsIdentity GetIdentity(dynamic identities)
        {
            string email = identities.Email;
            string role = identities.Role;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    MyCryptography.Encrypt(AuthOptions.JwtEmailEncryption, email)
                ),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token",
                "Email",
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
