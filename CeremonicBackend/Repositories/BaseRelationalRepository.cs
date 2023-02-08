using System.Linq;
using System.Threading.Tasks;
using System;

using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;

namespace CeremonicBackend.Repositories
{
    public class BaseRelationalRepository<Entity, IdType> : IBaseRepository<Entity, IdType> where Entity : BaseEntity<IdType>
    {
        protected CeremonicRelationalDbContext _db;
        public BaseRelationalRepository(CeremonicRelationalDbContext db)
        {
            _db = db;
        }

        public async Task<Entity> GetById(IdType id)
            => await _db.Set<Entity>().FindAsync(id);

        public async Task<IQueryable<Entity>> GetByPredicate(Func<Entity, bool> predicate)
            => await Task.Run(() => _db.Set<Entity>().Where(predicate ?? (e => true)).AsQueryable());

        public async Task<Entity> Add(Entity entity)
            => (await _db.Set<Entity>().AddAsync(entity)).Entity;

        public async Task<Entity> Update(Entity entity)
            => await Task.Run(() => _db.Set<Entity>().Update(entity).Entity);

        public async Task<Entity> Delete(IdType id)
        {
            var deletingEntity = await _db.Set<Entity>().FindAsync(id);

            _db.Set<Entity>().Remove(deletingEntity);

            return deletingEntity;
        }
    }
}
