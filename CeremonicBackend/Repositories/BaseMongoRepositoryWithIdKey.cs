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
        protected string _collectionName;
        public BaseMongoRepositoryWithIdKey(ICeremonicMongoDbContext db, string collectionName)
        {
            _db = db;
        }

        public async Task<Entity> GetById(IdType id)
            => (await _db.Database.GetCollection<Entity>(_collectionName)
            .FindAsync(e => e.Id.Equals(id))).FirstOrDefault();

        public async Task<IQueryable<Entity>> GetByPredicate(Func<Entity, bool> predicate)
            => (await _db.Database.GetCollection<Entity>(_collectionName)
            .FindAsync(e => predicate(e))).ToList().AsQueryable();

        public async Task<Entity> Add(Entity entity)
        {
            await Task.Run(() => { });

            _db.Database.GetCollection<Entity>(_collectionName)
                .InsertOne(entity);

            return entity;
        }

        public async Task<Entity> Update(Entity entity)
        {
            await Task.Run(() => { });

            _db.Database.GetCollection<Entity>(_collectionName)
                .ReplaceOne(e => e.Id.Equals(entity.Id), entity);

            return entity;
        }

        public async Task<Entity> Delete(IdType id)
        {
            Entity entity = await GetById(id);

            _db.Database.GetCollection<Entity>(_collectionName)
                .DeleteOne(e => e.Id.Equals(id));

            return entity;
        }
    }
}
