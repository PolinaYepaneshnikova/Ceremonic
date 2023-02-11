// using Microsoft.IdentityModel.JsonWebTokens;

using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;

using CeremonicBackend.DB.Relational;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Authentification;
using CeremonicBackend.Mappings;

namespace CeremonicBackend.Services
{
    public class AccountService : IAccountService
    {
        protected IUnitOfWork _UoW { get; set; }
        public AccountService(IUnitOfWork uow)
        {
            _UoW = uow;
        }



        public async Task<JwtApiModel> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException($"{nameof(email)}");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"{nameof(password)}");
            }

            UserEntity user = await _UoW.UserRepository.GetByEmail(email);
            if (user is null)
            {
                throw new NotFoundAppException($"user not found");
            }

            string inputHashPass = HashPassword(password);
            string userHashPass = await _UoW.UserRepository.GetHashPasswordById(user.Id);
            if (userHashPass != inputHashPass)
            {
                throw new NotFoundAppException($"uncorrect password");
            }

            ClaimsIdentity claims = GetIdentity(email, "User");
            string jwtString = JwtTokenizer.GetEncodedJWT(claims, AuthOptions.Lifetime);

            return new JwtApiModel(jwtString);
        }

        public async Task<UserApiModel> Registration(RegistrationApiModel dto)
        {
            if (string.IsNullOrEmpty(dto.Email))
            {
                throw new ArgumentException($"{nameof(dto.Email)}");
            }
            if (string.IsNullOrEmpty(dto.Password))
            {
                throw new ArgumentException($"{nameof(dto.Password)} not specified");
            }

            UserEntity user = await _UoW.UserRepository.GetByEmail(dto.Email);
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
                    Email = dto.Email,
                    PasswordHash = HashPassword(dto.Password),
                },
            };
            user = await _UoW.UserRepository.Add(user);
            await _UoW.SaveChanges();

            return user.ToUserApiModel();
        }



        static string HashPassword(string password)
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

        public static ClaimsIdentity GetIdentity(string email, string role)
        {
            var claims = new List<Claim>
            {
                new Claim("Email", email),
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
