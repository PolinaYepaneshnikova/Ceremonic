using System.Threading.Tasks;

using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserApiModel> Get(string email);
        Task<string> GetEmailById(int id);
        Task<string> GetRoleByEmail(string email);
    }
}
