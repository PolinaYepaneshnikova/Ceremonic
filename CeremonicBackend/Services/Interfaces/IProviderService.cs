using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IProviderService
    {
        Task<ProviderEntity> CreateForUser(UserEntity user, ProviderInfoApiModel providerInfo);
        Task<ProviderEntity> Edit(ProviderEditApiModel model);
    }
}
