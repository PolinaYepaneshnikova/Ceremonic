using System;
using System.Linq;
using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;
using SharpCompress.Common;

namespace CeremonicBackend.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        protected CeremonicRelationalDbContext _relationalDb { get; set; }
        protected ICeremonicMongoDbContext _mongoDb { get; set; }

        protected IBaseRepository<DB.Relational.ServiceEntity, int> _relationalRepository { get; set; }
        protected IBaseRepository<DB.Mongo.ServiceEntity, int> _mongoRepository { get; set; }

        public ServiceRepository(CeremonicRelationalDbContext relationalDb, ICeremonicMongoDbContext mongoDb)
        {
            _relationalDb = relationalDb;
            _mongoDb = mongoDb;

            _relationalRepository = new BaseRelationalRepository<DB.Relational.ServiceEntity, int>(_relationalDb);
            _mongoRepository = new BaseMongoRepositoryWithIdKey<DB.Mongo.ServiceEntity, int>(_mongoDb, "services");
        }

        public async Task<GeneralServiceEntity> Add(GeneralServiceEntity entity)
        {
            GeneralServiceEntity generalEntity = new GeneralServiceEntity();

            generalEntity.RelationalServiceEntity = await _relationalRepository.Add(entity.RelationalServiceEntity);
            generalEntity.MongoServiceEntity = await _mongoRepository.Add(entity.MongoServiceEntity);

            return generalEntity;
        }

        public async Task<GeneralServiceEntity> Delete(int id)
        {
            GeneralServiceEntity generalEntity = new GeneralServiceEntity();

            generalEntity.RelationalServiceEntity = await _relationalRepository.Delete(id);
            generalEntity.MongoServiceEntity = await _mongoRepository.Delete(id);

            return generalEntity;
        }

        public async Task<GeneralServiceEntity> GetById(int id)
        {
            GeneralServiceEntity generalEntity = new GeneralServiceEntity();

            generalEntity.RelationalServiceEntity = await _relationalRepository.GetById(id);
            generalEntity.MongoServiceEntity = await _mongoRepository.GetById(id);

            return generalEntity;
        }

        public async Task<IQueryable<GeneralServiceEntity>> GetByPredicate(Func<GeneralServiceEntity, bool> predicate)
        {
            await Task.Run(() => { });

            IQueryable<GeneralServiceEntity> generalServicesSet =
                _relationalDb.Set<DB.Relational.ServiceEntity>().Select(e => new GeneralServiceEntity()
                {
                    Id = e.Id,
                    RelationalServiceEntity = e,
                    MongoServiceEntity = _mongoRepository.GetById(e.Id).Result,
                });

            return generalServicesSet.Where(predicate ?? (e => true)).AsQueryable();
        }

        public async Task<GeneralServiceEntity> Update(GeneralServiceEntity entity)
        {
            GeneralServiceEntity generalEntity = new GeneralServiceEntity();

            generalEntity.RelationalServiceEntity = await _relationalRepository.Update(entity.RelationalServiceEntity);
            generalEntity.MongoServiceEntity = await _mongoRepository.Update(entity.MongoServiceEntity);

            return generalEntity;
        }

        public async Task<GeneralServiceEntity> GetByName(string name)
        {
            await Task.Run(() => { });

            DB.Relational.ServiceEntity relationalServiceEntity = _relationalDb.Services
                .Where(e => e.Name == name).FirstOrDefault();

            DB.Mongo.ServiceEntity mongoServiceEntity;

            mongoServiceEntity = await _mongoRepository.GetById(relationalServiceEntity.Id);

            GeneralServiceEntity generalServiceEntity = new GeneralServiceEntity()
            {
                Id = relationalServiceEntity.Id,
                RelationalServiceEntity = relationalServiceEntity,
                MongoServiceEntity = mongoServiceEntity,
            };

            return generalServiceEntity;
        }
    }
}
