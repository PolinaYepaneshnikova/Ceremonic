using System.Threading.Tasks;

using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IAccountService
    {
        Task<JwtApiModel> Login(string email, string password);
        Task<UserApiModel> Registration(RegistrationApiModel dto);
    }
}
