﻿using System.Linq;
using System.Threading.Tasks;
using System;

using CeremonicBackend.DB.Relational;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IBaseRepository<Entity, IdType>
    {
        Task<Entity> GetById(IdType id);
        Task<IQueryable<Entity>> GetByPredicate(Func<Entity, bool> predicate);

        Task<Entity> Add(Entity entity);
        Task<Entity> Update(Entity entity);
        Task<Entity> Delete(IdType id);
    }
}
