using CeremonicBackend.DB.Relational;
using System.Linq;
using System.Threading.Tasks;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IAgreementRepository : IBaseRepository<AgreementEntity, int>
    {
        Task<IQueryable<AgreementEntity>> GetByClientId(int clientId);
        Task<IQueryable<AgreementEntity>> GetByProviderId(int providerId);
    }
}
