using System.Threading.Tasks;

using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IAccountService
    {
        IUserCreatorService UserCreatorService { get; set; }

        Task<JwtApiModel> Login();
        Task<JwtApiModel> Registration();
    }
}
