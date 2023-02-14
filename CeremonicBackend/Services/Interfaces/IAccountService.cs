using CeremonicBackend.WebApiModels;
using System.Threading.Tasks;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IAccountService
    {
        Task<JwtApiModel> Login(string email, string password);
        Task<JwtApiModel> Registration(RegistrationApiModel dto);
    }
}
