using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;

namespace CeremonicBackend.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected CeremonicRelationalDbContext _relationalDb { get; set; }
        protected ICeremonicMongoDbContext _mongoDb { get; set; }

        public IUserRepository UserRepository { get; protected set; }

        public IServiceRepository ServiceRepository { get; protected set; }

        public IWeddingRepository WeddingRepository { get; protected set; }
        public IProviderRepository ProviderRepository { get; protected set; }

        public UnitOfWork(CeremonicRelationalDbContext relationalDb, ICeremonicMongoDbContext mongoDb)
        {
            _relationalDb = relationalDb;
            _mongoDb = mongoDb;

            UserRepository = new UserRepository(relationalDb);

            ServiceRepository = new ServiceRepository(relationalDb, mongoDb);

            WeddingRepository = new WeddingRepository(mongoDb);
            ProviderRepository = new ProviderRepository(mongoDb);
        }

        public async Task<int> SaveChanges() => await _relationalDb.SaveChangesAsync();
    }
}
