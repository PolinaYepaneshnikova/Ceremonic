using CeremonicBackend.WebApiModels;
using System.Threading.Tasks;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IUserCreatorService
    {
        RegistrationApiModel Model { get; set; }

        Task<dynamic> Create();
    }
}