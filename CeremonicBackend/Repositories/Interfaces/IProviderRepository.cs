using System.Collections.Generic;
using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IProviderRepository : IBaseRepository<ProviderEntity, int>
    {
        Task<ProviderEntity> GetByEmail(string email);
        Task<List<ProviderEntity>> Search(SearchProviderApiModel model);
    }
}
