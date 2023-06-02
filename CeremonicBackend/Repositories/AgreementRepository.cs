using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;

namespace CeremonicBackend.Repositories
{
    public class AgreementRepository : BaseRelationalRepository<AgreementEntity, int>, IAgreementRepository
    {
        public AgreementRepository(CeremonicRelationalDbContext db, IUnitOfWork uow) : base(db, uow) { }
    }
}
