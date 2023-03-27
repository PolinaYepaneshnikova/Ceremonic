using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

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

        public IFileRepository FileRepository { get; protected set; }

        public UnitOfWork(CeremonicRelationalDbContext relationalDb, ICeremonicMongoDbContext mongoDb)
        {
            _relationalDb = relationalDb;
            _mongoDb = mongoDb;

            UserRepository = new UserRepository(relationalDb, this);

            ServiceRepository = new ServiceRepository(relationalDb, mongoDb, this);

            WeddingRepository = new WeddingRepository(mongoDb, this);
            ProviderRepository = new ProviderRepository(mongoDb, this);
        }

        public void SetFileRepository(ControllerBase controller, IWebHostEnvironment env)
        {
            FileRepository = new FileRepository(controller, env);
        }

        public async Task<int> SaveChanges() => await _relationalDb.SaveChangesAsync();
    }
}
