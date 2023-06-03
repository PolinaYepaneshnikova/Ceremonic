using System.Collections.Generic;
using System.Threading.Tasks;

using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task Add(string userEmail, int providerId);
        Task Delete(string userEmail, int providerId);
        Task<List<ProviderApiModel>> GetAll(string userEmail);
    }
}
