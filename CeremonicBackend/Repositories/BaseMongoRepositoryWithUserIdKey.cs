using System.Linq;
using System.Threading.Tasks;
using System;

using MongoDB.Driver;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.Repositories.Interfaces;

namespace CeremonicBackend.Repositories
{
    public class BaseMongoRepositoryWithUserIdKey<Entity> : IBaseRepository<Entity, int> where Entity : JoinedToUserEntity
    {
        protected ICeremonicMongoDbContext _db;
        protected IUnitOfWork _UoW;

        public string CollectionName;
        public BaseMongoRepositoryWithUserIdKey(ICeremonicMongoDbContext db, IUnitOfWork uow, string collectionName)
        {
            _db = db;
            _UoW = uow;

            CollectionName = collectionName;
        }

        public async Task<Entity> GetById(int userId)
            => (await _db.Database.GetCollection<Entity>(CollectionName)
            .FindAsync(e => e.UserId.Equals(userId))).FirstOrDefault();

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
                .ReplaceOne(e => e.UserId.Equals(entity.UserId), entity);

            return entity;
        }

        public async Task<Entity> Delete(int userId)
        {
            Entity entity = await GetById(userId);

            _db.Database.GetCollection<Entity>(CollectionName)
                .DeleteOne(e => e.UserId.Equals(userId));

            return entity;
        }
    }
}
