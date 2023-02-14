using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;
using System.Threading.Tasks;

namespace CeremonicBackend.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected CeremonicRelationalDbContext _relationalDb { get; set; }
        protected ICeremonicMongoDbContext _mongoDb { get; set; }

        public IUserRepository UserRepository { get; set; }

        public UnitOfWork(CeremonicRelationalDbContext relationalDb, ICeremonicMongoDbContext mongoDb)
        {
            _relationalDb = relationalDb;
            _mongoDb = mongoDb;

            UserRepository = new UserRepository(relationalDb);
        }

        public async Task<int> SaveChanges() => await _relationalDb.SaveChangesAsync();
    }
}
