using System.Linq;
using System.Threading.Tasks;
using System;

using MongoDB.Driver;

using CeremonicBackend.DB.Relational;
using CeremonicBackend.DB.Mongo;
using CeremonicBackend.Repositories.Interfaces;

namespace CeremonicBackend.Repositories
{
    public class BaseMongoRepositoryWithIdKey<Entity, IdType> : IBaseRepository<Entity, IdType> where Entity : BaseEntity<IdType>
    {
        protected ICeremonicMongoDbContext _db;
        protected IUnitOfWork _UoW;

        public string CollectionName;
        public BaseMongoRepositoryWithIdKey(ICeremonicMongoDbContext db, IUnitOfWork uow, string collectionName)
        {
            _db = db;
            _UoW = uow;

            CollectionName = collectionName;
        }

        public async Task<Entity> GetById(IdType id)
            => (await _db.Database.GetCollection<Entity>(CollectionName)
            .FindAsync(e => e.Id.Equals(id))).FirstOrDefault();

        public async Task<IQueryable<Entity>> GetByPredicate(Func<Entity, bool> predicate)
            => (await _db.Database.GetCollection<Entity>(CollectionName)
            .FindAsync(e => predicate(e))).ToList().AsQueryable();

        public async Task<Entity> Add(Entity entity)
        {
            await Task.Run(() => { });

            _db.Database.GetCollection<Entity>(CollectionName)
                .InsertOne(entity);

            return entity;
        }

        public async Task<Entity> Update(Entity entity)
        {
            await Task.Run(() => { });

            _db.Database.GetCollection<Entity>(CollectionName)
                .ReplaceOne(e => e.Id.Equals(entity.Id), entity);

            return entity;
        }

        public async Task<Entity> Delete(IdType id)
        {
            Entity entity = await GetById(id);

            _db.Database.GetCollection<Entity>(CollectionName)
                .DeleteOne(e => e.Id.Equals(id));

            return entity;
        }
    }
}
