using CeremonicBackend.WebApiModels;
using System.Threading.Tasks;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IGoogleAccountService
    {
        Task<JwtApiModel> Login(string tokenId);
        Task<JwtApiModel> Registration(GoogleRegistrationApiModel model);
    }
}
