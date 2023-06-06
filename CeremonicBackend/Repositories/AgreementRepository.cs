using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;

using System.Linq;
using System.Threading.Tasks;

namespace CeremonicBackend.Repositories
{
    public class AgreementRepository : BaseRelationalRepository<AgreementEntity, int>, IAgreementRepository
    {
        public AgreementRepository(CeremonicRelationalDbContext db, IUnitOfWork uow) : base(db, uow) { }
        public async Task<IQueryable<AgreementEntity>> GetByClientId(int clientId)
            => await Task.Run(() => _db.Agreements.Where(a => a.ClientId == clientId));
        public async Task<IQueryable<AgreementEntity>> GetByProviderId(int providerId)
            => await Task.Run(() => _db.Agreements.Where(a => a.ProviderId == providerId));
    }
}
